using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Dependiente
    {
        public int IdDependiente { get; set; }

        [Required]
        [Display(Name = "Nombre Dependiente")]
        public string Nombre { get; set; }

        [Required]
        [Display(Name = "Apellido Paterno")]
        public string ApellidoPaterno { get; set; }

        [Display(Name = "Apellido Materno")]
        public string ApellidoMaterno { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de Nacimiento")]
        public DateTime FechaNacimiento { get; set; }

        [Display(Name = "Estado Civil")]
        public string EstadoCivil { get; set; }
        public string Genero { get; set; }

        [Required]
        [Phone]
        public string Telefono { get; set; }
        public string RFC { get; set; }
        public List<object> Dependientes { get; set; }
        public ML.Empleado Empleado { get; set; }
    }
}
