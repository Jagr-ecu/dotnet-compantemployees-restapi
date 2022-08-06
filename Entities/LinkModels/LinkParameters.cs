using Microsoft.AspNetCore.Http;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.LinkModels
{
    //Vamos a usar este registro para transferir los parámetros requeridos de nuestro controlador a la capa
    //de servicio y evitar la instalación de un paquete NuGet adicional.
    public record LinkParameters(EmployeeParameters EmployeeParameters, HttpContext Context);
}
