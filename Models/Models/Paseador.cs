using System;
using System.Collections.Generic;

namespace Models.Models;

public partial class Paseador
{
    public int IdPaseador { get; set; }

    public string DniPaseador { get; set; }

    public string Nombre { get; set; }

    public string Apellido { get; set; }

    public string Direccion { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public string TelefonoPaseador { get; set; }

    public double Latitud { get; set; }

    public double Longitud { get; set; }

    public virtual ICollection<Opinione> Opiniones { get; set; } = new List<Opinione>();

    public virtual ICollection<Precio> Precios { get; set; } = new List<Precio>();

    public virtual ICollection<Ranking> Rankings { get; set; } = new List<Ranking>();

    public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
}
