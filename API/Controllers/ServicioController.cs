using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.Context;
using Models.DTOs;
using Models.Interfaces;
using Models.Models;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicioController : BaseApiController
    {
        private readonly DogWalkPlusContext _context;

        public ServicioController(DogWalkPlusContext context)
        {
            _context = context;
        }

        [HttpGet("nombres")]
        public async Task<ActionResult<IEnumerable<string>>> GetNombresServicios()
        {
            return await _context.Servicios.Select(s => s.NombreServicio).ToListAsync();
        }


        [HttpGet("servicios")]
        public async Task<ActionResult<IEnumerable<Servicio>>> GetServicios()
        {
            return await _context.Servicios.ToListAsync();
        }

        [HttpGet("servicios/{nombre_servicio}")] //GET api/servicios/5 Obtenemos un servicio con un id específico
        public async Task<ActionResult<Servicio>> GetServicio(string nombreServicio)
        {
            var servicio = await _context.Servicios.FindAsync(nombreServicio);

            if (servicio == null)
            {
                return NotFound();
            }

            return servicio;
        }

        [HttpPost("servicios")] //POST api/servicios Creamos un nuevo servicio
        public async Task<ActionResult<Servicio>> PostServicio(Servicio servicio)
        {
            _context.Servicios.Add(servicio);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetServicio", new { nombre = servicio.NombreServicio }, servicio);
        }

        [HttpPut("servicios/{nombre_servicio}")] //PUT api/servicios/ Actualizamos un servicio con un nombre específico
        public async Task<IActionResult> PutServicio(string nombreServicio, string descripcionServicio, Servicio servicio)
        {
            if (nombreServicio != servicio.NombreServicio && descripcionServicio != servicio.DescripcionServicio)
            {
                return BadRequest();
            }

            _context.Entry(servicio).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServicioExists(nombreServicio, descripcionServicio))
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

        private bool ServicioExists(string nombreServicio, string descripcionServicio)
        {
            return _context.Servicios.Any(e => e.NombreServicio == nombreServicio && e.DescripcionServicio == descripcionServicio);
        }

        [HttpDelete("servicios/{nombre_servicio}")] //DELETE api/servicios/ Eliminamos un servicio con un nombre específico
        public async Task<IActionResult> DeleteServicio(string nombreServicio)
        {
            var servicio = await _context.Servicios.FindAsync(nombreServicio);
            if (servicio == null)
            {
                return NotFound();
            }

            _context.Servicios.Remove(servicio);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
