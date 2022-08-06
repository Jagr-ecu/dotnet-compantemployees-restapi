using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Employee
    {
        [Column("EmployeeId")] 
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Nombre del empleado es requerido")]
        [MaxLength(30, ErrorMessage = "El nombre tiene un tamaño máximo de caracteres de 30")] 
        public string? Name { get; set; }

        [Required(ErrorMessage = "Edad es requerido")] 
        public int Age { get; set; }

        [Required(ErrorMessage = "Cargo es requerido.")]
        [MaxLength(20, ErrorMessage = "El cargo tiene un tamaño máximo de caracteres de 20.")] 
        public string? Position { get; set; }

        [ForeignKey(nameof(Company))] 
        public Guid CompanyId { get; set; }

        public Company? Company { get; set; }
    }
}
