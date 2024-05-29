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
    public class OpinionController : BaseApiController
    {
        private readonly DogWalkPlusContext _context;

        public OpinionController(DogWalkPlusContext context)
        {
            _context = context;
        }

        [HttpGet("opiniones")]
        public async Task<ActionResult<IEnumerable<OpinionDto>>> GetOpiniones()
        {
            var opiniones = await _context.Opiniones
                .Select(o => new OpinionDto
                {
                    NombrePaseador = o.IdPaseadorNavigation.Nombre,
                    NombrePerro = o.IdPerroNavigation.Nombre,
                    Comentario = o.Comentario,
                    Puntuacion = o.Puntuacion
                })
                .ToListAsync();

            return opiniones;
        }


        [HttpGet("opiniones/{idOpinion}")]
        public async Task<ActionResult<Opinione>> GetOpinione(int idOpinion)
        {
            var opinion = await _context.Opiniones.FindAsync(idOpinion);

            if (opinion == null)
            {
                return NotFound();
            }

            return opinion;
        }

        [HttpPost("opiniones/crear")]
        public async Task<ActionResult<OpinionDto>> PostOpinion(CrearOpinionDto crearOpinionDto)
        {
            // Busca el Perro y el Paseador por sus nombres
            var perro = await _context.Perros.FirstOrDefaultAsync(p => p.Nombre == crearOpinionDto.NombrePerro);
            var paseador = await _context.Paseadors.FirstOrDefaultAsync(p => p.Nombre == crearOpinionDto.NombrePaseador);

            if (perro == null || paseador == null)
            {
                return NotFound("El Perro o el Paseador no se encontraron");
            }

            var opinion = new Opinione
            {
                Puntuacion = crearOpinionDto.Puntuacion,
                Comentario = crearOpinionDto.Comentario,
                IdPerro = perro.IdPerro,
                IdPaseador = paseador.IdPaseador
            };

            _context.Opiniones.Add(opinion);
            await _context.SaveChangesAsync();

            var opinionDto = new OpinionDto
            {
                NombrePaseador = paseador.Nombre,
                NombrePerro = perro.Nombre,
                Comentario = opinion.Comentario,
                Puntuacion = opinion.Puntuacion,
            };

            return CreatedAtAction("GetOpiniones", new { id = opinion.IdOpinion }, opinionDto);
        }



        [HttpPut("opiniones/{idOpinion}")]
        public async Task<IActionResult> PutOpinione(int idOpinion, Opinione opinion)
        {
            if (idOpinion != opinion.IdOpinion)
            {
                return BadRequest();
            }

            _context.Entry(opinion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OpinioneExists(idOpinion))
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

        [HttpDelete("opiniones/{idOpinion}")]
        public async Task<IActionResult> DeleteOpinione(int idOpinion)
        {
            var opinion = await _context.Opiniones.FindAsync(idOpinion);
            if (opinion == null)
            {
                return NotFound();
            }

            _context.Opiniones.Remove(opinion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OpinioneExists(int idOpinion)
        {
            return _context.Opiniones.Any(e => e.IdOpinion == idOpinion);
        }

    }
}
