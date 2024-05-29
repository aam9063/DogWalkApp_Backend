using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs
{
    public class ReservaDTO
    {
        public int IdUsuario { get; set; } // Agregado
        public int IdPerro { get; set; } // Agregado
        public string NombrePaseador { get; set; }
        public string Servicio { get; set; }
        public DateTime FechaHora { get; set; }
    }
}
