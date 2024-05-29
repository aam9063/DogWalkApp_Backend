using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs
{
    public class CrearOpinionDto
    {
        public string NombrePaseador { get; set; }
        public string NombrePerro { get; set; }
        public string Comentario { get; set; }
        public int Puntuacion { get; set; }
    }
}
