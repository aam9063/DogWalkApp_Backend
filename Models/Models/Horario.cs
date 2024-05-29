using System;
using System.Collections.Generic;

namespace Models.Models;

public partial class Horario
{
    public int IdHorario { get; set; }

    public DateTime FechaHora { get; set; }

    public string Disponibilidad { get; set; }

    public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
}
