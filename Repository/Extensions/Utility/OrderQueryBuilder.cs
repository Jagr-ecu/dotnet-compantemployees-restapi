using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Extensions.Utility
{
    public static class OrderQueryBuilder
    {
        public static string CreateOrderQuery<T>(string orderByQueryString)
        {
            var orderParams = orderByQueryString.Trim().Split(',');
            var propertyInfos = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);//propiedades o campos de la bd para poder verificar
            var orderQueryBuilder = new StringBuilder();

            foreach (var param in orderParams)
            {
                if (string.IsNullOrWhiteSpace(param))
                    continue;

                var propertyFromQueryName = param.Split(" ")[0];//separa la primera palabra
                var objectProperty = propertyInfos.FirstOrDefault(pi =>
                    pi.Name.Equals(propertyFromQueryName, StringComparison.InvariantCultureIgnoreCase));//verfica si la propiedad que se puso en el query existe

                if (objectProperty == null)
                    continue;

                var direction = param.EndsWith("desc") ? "descending" : "ascending";//verifica que al final este escrito desc

                orderQueryBuilder.Append($"{objectProperty.Name.ToString()} {direction}, ");
            }

            var orderQuery = orderQueryBuilder.ToString().TrimEnd(',', ' ');

            return orderQuery;
        }
    }
}
