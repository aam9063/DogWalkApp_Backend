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
    public class PrecioController : BaseApiController
    {
        private readonly DogWalkPlusContext _context;

        public PrecioController(DogWalkPlusContext context)
        {
            _context = context;
        }

        [HttpGet("precios")]
        public async Task<ActionResult<IEnumerable<PrecioDto>>> GetPrecios()
        {
            var precios = await _context.Precios
                .Include(p => p.IdPaseadorNavigation)
                .Include(p => p.IdServicioNavigation)
                .Select(p => new PrecioDto
                {
                    NombrePaseador = p.IdPaseadorNavigation.Nombre,
                    NombreServicio = p.IdServicioNavigation.NombreServicio,
                    Precio = p.Precio1
                })
                .ToListAsync();

            return precios;
        }



        [HttpGet("precios/{idPaseador}/{idServicio}")]
        public async Task<ActionResult<Precio>> GetPrecio(int idPaseador, int idServicio)
        {
            var precio = await _context.Precios.FirstOrDefaultAsync(p => p.IdPaseador == idPaseador && p.IdServicio == idServicio);

            if (precio == null)
            {
                return NotFound();
            }

            return precio;
        }

        [HttpPost("precios")]
        public async Task<ActionResult<Precio>> PostPrecio(Precio precio)
        {
            _context.Precios.Add(precio);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPrecio", new { idPaseador = precio.IdPaseador, idServicio = precio.IdServicio }, precio);
        }

        [HttpPut("precios/{idPaseador}/{idServicio}")]
        public async Task<IActionResult> PutPrecio(int idPaseador, int idServicio, Precio precio)
        {
            if (idPaseador != precio.IdPaseador || idServicio != precio.IdServicio)
            {
                return BadRequest();
            }

            _context.Entry(precio).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PrecioExists(idPaseador, idServicio))
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

        [HttpDelete("precios/{idPaseador}/{idServicio}")]
        public async Task<IActionResult> DeletePrecio(int idPaseador, int idServicio)
        {
            var precio = await _context.Precios.FirstOrDefaultAsync(p => p.IdPaseador == idPaseador && p.IdServicio == idServicio);
            if (precio == null)
            {
                return NotFound();
            }

            _context.Precios.Remove(precio);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PrecioExists(int idPaseador, int idServicio)
        {
            return _context.Precios.Any(e => e.IdPaseador == idPaseador && e.IdServicio == idServicio);
        }


    }
}
