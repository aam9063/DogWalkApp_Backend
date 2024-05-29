using System;
using System.Collections.Generic;

namespace Models.Models;

public partial class Foto
{
    public int IdFoto { get; set; }

    public int? IdPerro { get; set; }

    public string UrlFoto { get; set; }

    public string Descripcion { get; set; }

    public virtual Perro IdPerroNavigation { get; set; }
}
