using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Models.Models;

public partial class Reserva
{
    public int IdReserva { get; set; }

    public int IdUsuario { get; set; }

    public int IdPaseador { get; set; }

    public int IdServicio { get; set; }
    public int IdPerro { get; set; }

    public int IdHorario { get; set; }

    public DateTime FechaReserva { get; set; }

    public string EstadoReserva { get; set; }

    public virtual Horario IdHorarioNavigation { get; set; }

    public virtual Paseador IdPaseadorNavigation { get; set; }

    public virtual Perro IdPerroNavigation { get; set; }

    public virtual Servicio IdServicioNavigation { get; set; }

    public virtual Usuario IdUsuarioNavigation { get; set; }
}
