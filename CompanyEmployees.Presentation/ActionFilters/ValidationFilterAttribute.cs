using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyEmployees.Presentation.ActionFilters
{
    public class ValidationFilterAttribute : IActionFilter
    {
        public ValidationFilterAttribute()
        {

        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var action = context.RouteData.Values["action"];
            var controller = context.RouteData.Values["controller"];

            var param = context.ActionArguments
                .SingleOrDefault(x => x.Value.ToString().Contains("Dto")).Value;

            if(param is null)
            {
                context.Result = new BadRequestObjectResult($"Objeto es nulo. Controller: {controller}," +
                    $"action: {action}");
                return;
            }

            if (!context.ModelState.IsValid)
                context.Result = new UnprocessableEntityObjectResult(context.ModelState);
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {

        }
    }
}

//Con esto filter podemos quitar las validaciones en los controladores
//if (company is null)
//    return BadRequest("El objeto company es nulo");

//if (!ModelState.IsValid)
//    return UnprocessableEntity(ModelState);
