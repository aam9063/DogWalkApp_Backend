using Microsoft.AspNetCore.Authorization;
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
    public class UsuarioController : BaseApiController
    {
        private readonly DogWalkPlusContext _context;
        private readonly ITokenServicio _tokenServicio;

        public UsuarioController(DogWalkPlusContext context, ITokenServicio tokenServicio)
        {
            _context = context;
            _tokenServicio = tokenServicio;
        }

        [HttpGet("usuarios")] // GET: api/usuarios Obtenemos todos los usuarios
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
            return await _context.Usuarios.ToListAsync();
        }

        [Authorize]
        [HttpGet("usuarios/{id}")] // GET: api/usuarios/5 Aqui obtenemos un usuario con un id específico
        public async Task<ActionResult<Usuario>> GetUsuario(int idUsuario)
        {
            var usuario = await _context.Usuarios.FindAsync(idUsuario);

            if (usuario == null)
            {
                return NotFound();
            }

            return usuario;
        }

        [HttpPut("usuarios/{email}")]
        public async Task<IActionResult> PutUsuario(string email, ActUsuarioDto usuarioDto)
        {
            // Busca el usuario en la base de datos que coincida con el correo electrónico proporcionado en la ruta.
            var usuarioActual = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
            if (usuarioActual == null)
            {
                return NotFound();
            }

            // Valida los datos recibidos.
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Actualiza los datos del usuario.
            usuarioActual.DniUsuario = usuarioDto.DniUsuario;
            usuarioActual.Nombre = usuarioDto.Nombre;
            usuarioActual.Apellido = usuarioDto.Apellido;
            usuarioActual.Direccion = usuarioDto.Direccion;
            usuarioActual.Email = usuarioDto.Email;
            usuarioActual.Password = usuarioDto.Password;
            usuarioActual.TelefonoUsuario = usuarioDto.TelefonoUsuario;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return NoContent();
        }



        [HttpPost("registro")] 
        public async Task<ActionResult<UsuarioDto>> Registro(RegistroDto registroDto)
        {
            if (await UsuarioExiste(registroDto.Email))
            {
                return BadRequest("El usuario ya existe");
            }

            var usuario = new Usuario
            {
                DniUsuario = registroDto.DniUsuario,
                Nombre = registroDto.Nombre,
                Apellido = registroDto.Apellido,
                Direccion = registroDto.Direccion,
                Email = registroDto.Email,
                Password = registroDto.Password,
                TelefonoUsuario = registroDto.TelefonoUsuario
            };
            
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return new UsuarioDto
            {
                Email = usuario.Email,
                Token = _tokenServicio.CrearToken(usuario)
            };
        }
      
        private async Task<bool> UsuarioExiste(string username)
        {
            return await _context.Usuarios.AnyAsync(x => x.Email == username.ToLower());
        }

        [HttpPost("login")] // POST: api/usuarios/login Aqui hacemos login con un usuario escrito en el body en formato JSON en Postman
        public async Task<ActionResult<UsuarioDto>> Login(LoginDto loginDto)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(x => x.Email == loginDto.Email);

            if (usuario == null)
            {
                return Unauthorized("Usuario no existe");
            }

            if (usuario.Password != loginDto.Password)
            {
                return Unauthorized("Contraseña incorrecta");
            }

            return new UsuarioDto
            {
                Email = usuario.Email,
                Token = _tokenServicio.CrearToken(usuario)
            };
        }

        [HttpPost("recuperar-contrasenya")]
        public async Task<ActionResult> RecuperarContraseña(RecuperarDto recuperarDto)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == recuperarDto.Email);

            if (usuario == null)
            {
                return NotFound();
            }

            // Genera un token de restablecimiento de contraseña.
            var token = GenerarTokenDeRestablecimientoDeContraseña();

            // Crea un enlace para restablecer la contraseña.
            var enlace = $"http://localhost:4200/recuperar?token={token}";

            // Envía un correo electrónico al usuario con el enlace para restablecer la contraseña.
            await EnviarCorreoElectronico(usuario.Email, "Restablecer contraseña", $"Haz clic en este enlace para restablecer tu contraseña: {enlace}");

            return Ok();
        }

        private async Task EnviarCorreoElectronico(string email, string asunto, string mensaje)
        {
            using (var client = new System.Net.Mail.SmtpClient("smtp.gmail.com", 587)) // 587 es el puerto para conexiones TLS/STARTTLS
            {
                client.EnableSsl = true; // Habilita SSL
                client.UseDefaultCredentials = false; // Permite definir las credenciales manualmente
                client.Credentials = new System.Net.NetworkCredential("dogwalkapp207@gmail.com", "2ZE868Fru"); // Define las credenciales

                var correo = new System.Net.Mail.MailMessage("dogwalkapp207@gmail.com", email, asunto, mensaje);
                await client.SendMailAsync(correo);
            }
        }






        private string GenerarTokenDeRestablecimientoDeContraseña()
        {
            using (var randomNumberGenerator = System.Security.Cryptography.RandomNumberGenerator.Create())
            {
                var randomNumber = new byte[20]; // 20 bytes will give us a 40 characters long token
                randomNumberGenerator.GetBytes(randomNumber);
                return BitConverter.ToString(randomNumber).Replace("-", "");
            }
        }



        [HttpDelete("usuarios/{id}")]
        public async Task<IActionResult> DeleteUsuario(int idUsuario)
        {
            var usuario = await _context.Usuarios.FindAsync(idUsuario);
            if (usuario == null)
            {
                return NotFound();
            }

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

       
    }
}
