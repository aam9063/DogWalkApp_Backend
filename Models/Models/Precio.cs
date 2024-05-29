using System;
using System.Collections.Generic;

namespace Models.Models;

public partial class Precio
{
    public int IdPaseador { get; set; }

    public int IdServicio { get; set; }

    public decimal Precio1 { get; set; }

    public virtual Paseador IdPaseadorNavigation { get; set; }

    public virtual Servicio IdServicioNavigation { get; set; }
}
