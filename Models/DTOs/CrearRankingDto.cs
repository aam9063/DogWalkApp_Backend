﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs
{
    public class CrearRankingDto
    {
        public string NombreUsuario { get; set; }
        public string NombrePaseador { get; set; }
        public string Comentario { get; set; }
        public int Valoracion { get; set; }
    }

}
