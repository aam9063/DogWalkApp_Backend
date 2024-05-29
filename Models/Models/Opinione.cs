using System;
using System.Collections.Generic;

namespace Models.Models;

public partial class Opinione
{
    public int IdOpinion { get; set; }

    public int IdPerro { get; set; }

    public int IdPaseador { get; set; }

    public int Puntuacion { get; set; }

    public string Comentario { get; set; }

    public virtual Paseador IdPaseadorNavigation { get; set; }

    public virtual Perro IdPerroNavigation { get; set; }
}
