using System;
using System.Collections.Generic;

namespace Models.Models;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string DniUsuario { get; set; }

    public string Nombre { get; set; }

    public string Apellido { get; set; }

    public string Direccion { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public string TelefonoUsuario { get; set; }

 

    public virtual ICollection<Perro> Perros { get; set; } = new List<Perro>();

    public virtual ICollection<Ranking> Rankings { get; set; } = new List<Ranking>();

    public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();

  
}
