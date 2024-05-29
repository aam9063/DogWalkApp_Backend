using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace Models.DTOs
{
    public class ActUsuarioDto
    {
        public string DniUsuario { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Direccion { get; set; }
        public string Email { get; set; }

        [StringLength(10, MinimumLength = 4, ErrorMessage = "El password debe ser mínimo 4 Máximo 10 caracteres")]
        public string Password { get; set; }
        public string TelefonoUsuario { get; set; }

    }
}