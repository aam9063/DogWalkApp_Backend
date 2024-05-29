using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Context;
using Models.Models;
using Microsoft.EntityFrameworkCore;


namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FotoController : BaseApiController
    {
        private readonly DogWalkPlusContext _context;

        public FotoController(DogWalkPlusContext context)
        {
            _context = context;
        }

        [HttpGet("fotos")]
        public async Task<ActionResult<IEnumerable<Foto>>> GetFotos()
        {
            return await _context.Fotos.ToListAsync();
        }

        [HttpGet("fotos/{idFoto}")]
        public async Task<ActionResult<Foto>> GetFoto(int idFoto)
        {
            var foto = await _context.Fotos.FindAsync(idFoto);

            if (foto == null)
            {
                return NotFound();
            }

            return foto;
        }

        [HttpPost("fotos")]
        public async Task<ActionResult<Foto>> PostFoto(Foto foto)
        {
            _context.Fotos.Add(foto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFoto", new { idFoto = foto.IdFoto }, foto);
        }

        [HttpPut("fotos/{idFoto}")]
        public async Task<IActionResult> PutFoto(int idFoto, Foto foto)
        {
            if (idFoto != foto.IdFoto)
            {
                return BadRequest();
            }

            _context.Entry(foto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FotoExists(idFoto))
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

        [HttpDelete("fotos/{idFoto}")]
        public async Task<IActionResult> DeleteFoto(int idFoto)
        {
            var foto = await _context.Fotos.FindAsync(idFoto);
            if (foto == null)
            {
                return NotFound();
            }

            _context.Fotos.Remove(foto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FotoExists(int idFoto)
        {
            return _context.Fotos.Any(e => e.IdFoto == idFoto);
        }

    }
}
