using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace Models.DTOs
{
    public class RegistroPaseadorDto
    {
        [Required(ErrorMessage = "El DNI es requerido")]
        public string DniPaseador { get; set; }

        [Required(ErrorMessage = "El nombre es requerido")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El apellido es requerido")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "La dirección es requerida")]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "El email es requerido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "La contraseña es requerida")]
        [StringLength(10, MinimumLength = 4, ErrorMessage = "El password debe ser mínimo 4 Máximo 10 caracteres")]
        public string Password { get; set; }

        [Required(ErrorMessage = "El teléfono es requerido")]
        public string TelefonoPaseador { get; set; }
    }
}
