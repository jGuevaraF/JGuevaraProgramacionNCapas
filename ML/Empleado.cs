using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Empleado
    {
        [Required]
        public string NumeroEmpleado { get; set; }
        public string RFC {  get; set; }

        [Required]
        public string Nombre { get; set; }

        [Display(Name = "Apellido Paterno")]
        [Required]
        public string ApellidoPaterno { get; set; }

        [Display(Name = "Apellido Materno")]
        public string ApellidoMaterno { get; set; }

        [Display(Name = "Correo Electrónico")]
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Teléfono")]
        [Phone]
        [Required]
        public string Telefono { get; set; }

        [Display(Name = "Fecha de Nacimiento")]
        [DataType(DataType.Date)]
        public DateTime FechaNacimiento { get; set; }

        public string NSS { get; set; }

        public DateTime FechaIngreso { get; set; }

        public string Foto { get; set; }

        public List<object> Empleados { get; set; }

        public ML.Empresa Empresa { get; set; }

        public string Accion { get; set; }


    }
}
