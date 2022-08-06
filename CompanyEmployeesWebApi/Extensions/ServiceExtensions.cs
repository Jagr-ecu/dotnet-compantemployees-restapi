using Contracts;
using LoggerService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Repository;
using Service;
using Service.Contracts;

namespace CompanyEmployeesWebApi.Extensions
{
    //Metodos para configurar los servicios y para que Program.cs se vea mas simple
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services) =>
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                    builder.AllowAnyOrigin() 
                    .AllowAnyMethod() 
                    .AllowAnyHeader()
                    .WithExposedHeaders("X-Pagination"));//permitir que cliente lea header X-Pagination que agregamos en acción,
                //Opciones mas restringidas
                //WithOrigins("https://example.com")
                //WithMethods("POST", "GET")
                //WithHeaders("accept", "content-type")
            });

        //Configuracion de IIS
        //no se inicializa propepiedas dentro de opciones por que los valores por defectos estan bien
        public static void ConfigureIISIntegration(this IServiceCollection services) =>
            services.Configure<IISOptions>(options =>
            {

            });

        //logger manager
        public static void ConfigureLoggerService(this IServiceCollection services) =>
            services.AddSingleton<ILoggerManager, LoggerManager>();

        public static void ConfigureRepositoryManager(this IServiceCollection services) =>
            services.AddScoped<IRepositoryManager, RepositoryManager>();

        public static void ConfigureServiceManager(this IServiceCollection services) =>
            services.AddScoped<IServiceManager, ServiceManager>();

        public static void ConfigureSqlContext(this IServiceCollection services,
        IConfiguration configuration) =>
            services.AddDbContext<RepositoryContext>(opts =>
                opts.UseSqlServer(configuration.GetConnectionString("sqlConnection")));

        public static IMvcBuilder AddCustomCSVFormatter(this IMvcBuilder builder) =>
            builder.AddMvcOptions(config => config.OutputFormatters.Add(new CsvOutputFormatter()));

        //Servicio para añadir un media type customizado (ej: application/json) para que no de error 406
        public static void AddCustomMediaTypes(this IServiceCollection services)
        {
            services.Configure<MvcOptions>(config =>
            {
                var systemTextJsonOuputFormatter = config.OutputFormatters.
                    OfType<SystemTextJsonOutputFormatter>()?.FirstOrDefault();

                if(systemTextJsonOuputFormatter != null)
                {
                    systemTextJsonOuputFormatter.SupportedMediaTypes
                       .Add("application/vnd.jagr.hateoas+json");//custom media type
                }

                var xmlOutputFormatter = config.OutputFormatters.
                    OfType<XmlDataContractSerializerOutputFormatter>()?.FirstOrDefault();

                if(xmlOutputFormatter != null)
                {
                    xmlOutputFormatter.SupportedMediaTypes.Add("application/vnd.jagr.hateoas+xml");
                }
            });
        }
        
    }
}
