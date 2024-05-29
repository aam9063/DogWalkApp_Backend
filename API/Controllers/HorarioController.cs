using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Context;
using Models.Models;
using Microsoft.EntityFrameworkCore;
using Models.DTOs;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HorarioController : BaseApiController
    {
        private readonly DogWalkPlusContext _context;

        public HorarioController(DogWalkPlusContext context)
        {
            _context = context;
        }

        [HttpGet("fechasHoras")]
        public async Task<ActionResult<IEnumerable<DateTime>>> GetFechasHorasHorarios()
        {
            return await _context.Horarios.Select(h => h.FechaHora).ToListAsync();
        }


        [HttpGet("horarios")]
        public async Task<ActionResult<IEnumerable<HorarioDto>>> GetHorarios()
        {
            var horarios = await _context.Horarios.ToListAsync();
            var horarioDtos = horarios.Select(h => new HorarioDto
            {
                FechaHora = h.FechaHora,
                Disponibilidad = h.Disponibilidad
            }).ToList();

            return horarioDtos;
        }


        [HttpGet("horarios/{id}")]
        public async Task<ActionResult<HorarioDto>> GetHorario(int id)
        {
            var horario = await _context.Horarios.FindAsync(id);

            if (horario == null)
            {
                return NotFound();
            }

            var horarioDto = new HorarioDto
            {
                FechaHora = horario.FechaHora,
                Disponibilidad = horario.Disponibilidad
            };

            return horarioDto;
        }

        [HttpPost("horarios")]
        public async Task<ActionResult<Horario>> PostHorario(HorarioDto horarioDto)
        {
            var horario = new Horario
            {
                FechaHora = horarioDto.FechaHora,
                Disponibilidad = horarioDto.Disponibilidad
            };

            _context.Horarios.Add(horario);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHorario", new { id = horario.IdHorario }, horario);
        }



        [HttpDelete("horarios/{id}")]
        public async Task<IActionResult> DeleteHorario(int id)
        {
            var horario = await _context.Horarios.FindAsync(id);
            if (horario == null)
            {
                return NotFound();
            }

            _context.Horarios.Remove(horario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HorarioExists(int id)
        {
            return _context.Horarios.Any(e => e.IdHorario == id);
        }
    }
}
