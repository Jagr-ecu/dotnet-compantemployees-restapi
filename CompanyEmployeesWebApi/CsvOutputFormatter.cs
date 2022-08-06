using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using Shared.DataTransferObjects;
using System.Text;

namespace CompanyEmployeesWebApi
{
    public class CsvOutputFormatter : TextOutputFormatter
    {
        //define qué tipo de medios debe analizar este formateador, así como las codificaciones
        public CsvOutputFormatter()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/csv"));
            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }

        //indica si este serializador puede escribir o no el tipo CompanyDto
        protected override bool CanWriteType(Type? type)
        {
            if(typeof(CompanyDto).IsAssignableFrom(type) ||
                typeof(IEnumerable<CompanyDto>).IsAssignableFrom(type))
            {
                return base.CanWriteType(type);
            }

            return false;
        }

        //construye la respuesta
        public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context,
            Encoding selectedEncoding)
        {
            var response = context.HttpContext.Response;
            var buffer = new StringBuilder();

            if(context.Object is IEnumerable<CompanyDto>)
            {
                foreach(var company in (IEnumerable<CompanyDto>)context.Object)
                {
                    FormatCsv(buffer, company);
                }
            }
            else
            {
                FormatCsv(buffer, (CompanyDto)context.Object);
            }

            await response.WriteAsync(buffer.ToString());
        }

        //formatea la respuesta como queremos
        private static void FormatCsv(StringBuilder buffer, CompanyDto company)
        {
            buffer.AppendLine($"{company.Id},\"{company.Name},\"{company.FullAddress}\"");
        }
    }
}
