using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Usuario
    {
        public int IdUsuario { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Nombre { get; set; }


        [Display(Name = "Apellido Paterno")]
        [Required(ErrorMessage ="El apellido Paterno es requerido")]
        [RegularExpression(@"[A-Za-z]", ErrorMessage = "No cumple")]
        public string ApellidoPaterno { get; set; }

        public string ApellidoMaterno { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Sexo { get; set; }

        public string Telefono { get; set; }

        public string Celular { get; set; }

        public DateTime FechaNacimiento { get; set; }
        public string CURP { get; set; }
        public string Imagen {  get; set; }
        public bool Status {  get; set; }
        public List<object> Usuarios { get; set; }
        public ML.Rol Rol { get; set; }
        public ML.Direccion Direccion { get; set; }
    }
}
