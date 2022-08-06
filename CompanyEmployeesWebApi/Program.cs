using CompanyEmployees.Presentation.ActionFilters;
using CompanyEmployeesWebApi.Extensions;
using CompanyEmployeesWebApi.Utility;
using Contracts;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Options;
using NLog;
using Service.DataShaping;
using Shared.DataTransferObjects;

var builder = WebApplication.CreateBuilder(args);

//Logger manager configracion : para que muestre mensajes de debug en modo de produccion
LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

// Add services to the container.
builder.Services.ConfigureCors();
builder.Services.ConfigureIISIntegration();
builder.Services.ConfigureLoggerService();
builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureServiceManager();
builder.Services.ConfigureSqlContext(builder.Configuration);
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddScoped<ValidationFilterAttribute>();//se añade de filter de validaciones personalizado
builder.Services.AddScoped<ValidateMediaTypeAttribute>();

builder.Services.AddScoped<IDataShaper<EmployeeDto>, DataShaper<EmployeeDto>>();
builder.Services.AddScoped<IEmployeeLinks, EmployeeLinks>();

builder.Services.AddControllers(config =>
{
    config.RespectBrowserAcceptHeader = true;//Content negotiation
    config.ReturnHttpNotAcceptable = true;//Json no sera formato default
    config.InputFormatters.Insert(0, GetJsonPatchInputFormatter());//usado para patch
}).AddXmlDataContractSerializerFormatters()
  .AddCustomCSVFormatter()
.AddApplicationPart(typeof(CompanyEmployees.Presentation.AssemblyReference).Assembly);

builder.Services.AddCustomMediaTypes();//service extension

var app = builder.Build();

// Configure the HTTP request pipeline.

var logger = app.Services.GetRequiredService<ILoggerManager>();
app.ConfigureExceptionHandler(logger);

if (app.Environment.IsProduction())
    app.UseHsts();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.All
});

app.UseAuthorization();

app.MapControllers();

app.Run();

//se agrega newtonsofjson sin remplazar system.text.json. Esta función configura la compatibilidad con
//JSON Patch usando Newtonsoft.Json y deja los otros formateadores sin cambios.
NewtonsoftJsonPatchInputFormatter GetJsonPatchInputFormatter() =>
    new ServiceCollection().AddLogging().AddMvc().AddNewtonsoftJson()
    .Services.BuildServiceProvider()
    .GetRequiredService<IOptions<MvcOptions>>().Value.InputFormatters
    .OfType<NewtonsoftJsonPatchInputFormatter>().First();
