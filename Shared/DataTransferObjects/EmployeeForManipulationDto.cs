using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public abstract record EmployeeForManipulationDto
    {
        [Required(ErrorMessage = "El nombre del empleado es un campo requerido")]
        [MaxLength(30, ErrorMessage = "La cantidad maxima de caracteres del nombre es de 30")]
        public string? Name { get; init; }

        [Required(ErrorMessage = "La edad del empleado es un campo requerido")]
        [Range(18, int.MaxValue, ErrorMessage = "La edad del empleado es requerida y no puede ser menor de 18")]
        public int Age { get; init; }

        [Required(ErrorMessage = "La posicion del empleado es un campo requerido")]
        [MaxLength(20, ErrorMessage = "La cantidad maxima de caracteres de la posicion es de 20")]
        public string? Position { get; init; }
    }
}
