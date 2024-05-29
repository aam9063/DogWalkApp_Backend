using System;
using System.Collections.Generic;

namespace Models.Models;

public partial class Ranking
{
    public int IdUsuario { get; set; }

    public int IdPaseador { get; set; }

    public string Comentario { get; set; }

    public int Valoracion { get; set; }

    public virtual Paseador IdPaseadorNavigation { get; set; }

    public virtual Usuario IdUsuarioNavigation { get; set; }
}
