using System;
using System.Collections.Generic;

namespace Models.Models;

public partial class Perro
{
    public int IdPerro { get; set; }

    public string Nombre { get; set; }

    public string Raza { get; set; }

    public int Edad { get; set; }

    public int IdUsuario { get; set; }

    public string Instagram { get; set; }

    public string Tiktok { get; set; }

    public virtual ICollection<Foto> Fotos { get; set; } = new List<Foto>();

    public virtual Usuario IdUsuarioNavigation { get; set; }

    public virtual ICollection<Opinione> Opiniones { get; set; } = new List<Opinione>();

    public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();

   
}
