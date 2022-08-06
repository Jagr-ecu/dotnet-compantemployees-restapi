using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    //Se establece los datos que va a tener cada tabla en la bd
    public class Company
    {
        [Column("CompanyId")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "El nombre de la compañia es requerido")] 
        [MaxLength(60, ErrorMessage = "El nombre tiene un tamaño máximo de caracteres de 60")]
        public string? Name { get; set; } 

        [Required(ErrorMessage = "La direccion es requerida")] 
        [MaxLength(60, ErrorMessage = "La direccion tiene un tamaño máximo de caracteres de 60")] 
        public string? Address { get; set; }

        public string? Country { get; set; } 

        public ICollection<Employee>? Employees { get; set; }
    }
}
