using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Context;
using Models.Models;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Models.DTOs;


namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservaController : BaseApiController
    {
        private readonly DogWalkPlusContext _context;

        public ReservaController(DogWalkPlusContext context)
        {
            _context = context;
        }

        [HttpGet("reservas")]
        public async Task<ActionResult<IEnumerable<ReservaDTO>>> GetReservas()
        {
            var reservas = await _context.Reservas.Include(r => r.IdPaseadorNavigation)
                                                  .Include(r => r.IdServicioNavigation)
                                                  .Include(r => r.IdHorarioNavigation)
                                                  .ToListAsync();
            var reservaDtos = reservas.Select(r => new ReservaDTO
            {
                IdUsuario = r.IdUsuario,
                IdPerro = r.IdPerro,
                NombrePaseador = r.IdPaseadorNavigation.Nombre,
                Servicio = r.IdServicioNavigation.NombreServicio,
                FechaHora = r.IdHorarioNavigation.FechaHora
            }).ToList();

            return reservaDtos;
        }


        [HttpGet("reservas/{id}")]
        public async Task<ActionResult<Reserva>> GetReserva(int id)
        {
            var reserva = await _context.Reservas.FindAsync(id);

            if (reserva == null)
            {
                return NotFound();
            }

            return reserva;
        }

        [HttpPost("reservas")]
        public async Task<ActionResult<ReservaDTO>> PostReserva(ReservaDTO reservaDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var reserva = await MapToReserva(reservaDto);

            _context.Reservas.Add(reserva);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReserva", new { id = reserva.IdReserva }, reservaDto);
        }


        private async Task<Reserva> MapToReserva(ReservaDTO reservaDto)
        {
            var paseador = await _context.Paseadors.FirstOrDefaultAsync(p => p.Nombre == reservaDto.NombrePaseador);
            var servicio = await _context.Servicios.FirstOrDefaultAsync(s => s.NombreServicio == reservaDto.Servicio);
            var horario = await _context.Horarios.FirstOrDefaultAsync(h => h.FechaHora == reservaDto.FechaHora);

            if (paseador == null || servicio == null || horario == null)
            {
                throw new Exception("Paseador, Servicio o Horario no encontrado");
            }

            return new Reserva
            {
                IdUsuario = reservaDto.IdUsuario,
                IdPaseador = paseador.IdPaseador,
                IdServicio = servicio.IdServicio,
                IdPerro = reservaDto.IdPerro,
                IdHorario = horario.IdHorario,
                FechaReserva = reservaDto.FechaHora,
                EstadoReserva = "Reservado" 
            };
        }



        [HttpPut("reservas/{id}")]
        public async Task<IActionResult> PutReserva(int id, Reserva reserva)
        {
            if (id != reserva.IdReserva)
            {
                return BadRequest();
            }

            _context.Entry(reserva).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReservaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("reservas/{id}")]
        public async Task<IActionResult> DeleteReserva(int id)
        {
            var reserva = await _context.Reservas.FindAsync(id);
            if (reserva == null)
            {
                return NotFound();
            }

            _context.Reservas.Remove(reserva);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReservaExists(int id)
        {
            return _context.Reservas.Any(e => e.IdReserva == id);
        }

    }
}
