using System;
using System.Collections.Generic;

namespace Models.Models;

public partial class Servicio
{
    public int IdServicio { get; set; }

    public string NombreServicio { get; set; }

    public string DescripcionServicio { get; set; }

    public virtual ICollection<Precio> Precios { get; set; } = new List<Precio>();

    public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
}
