using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public abstract record CompanyForManipulationDto
    {
        [Required(ErrorMessage = "El nombre de la compañia es requerido")]
        [MaxLength(60, ErrorMessage = "El nombre de la compañia tiene un tamaño máximo de 60 caracteres")]
        public string? Name { get; init; }

        [Required(ErrorMessage = "La direccion de la compañia es requerida")]
        [MaxLength(60, ErrorMessage = "La direccion de la compañia tiene un tamaño máximo de 60 caracteres")]
        public string? Address { get; init; }

        [Required(ErrorMessage = "El pais de la compañia es requerida")]
        public string? Country { get; init; }
    }
}
