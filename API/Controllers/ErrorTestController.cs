using Microsoft.AspNetCore.Mvc;
using Models.Context;
using Models.Models;
using API.Errores;


namespace API.Controllers
{
    public class ErrorTestController : BaseApiController
    {
        private readonly DogWalkPlusContext _context;

        public ErrorTestController(DogWalkPlusContext context)
        {
            _context = context;
        }


        
        [HttpGet("auth")]
        public ActionResult<string> GetNotAuthorize()
        {
            return "No autorizado";
        }

        
        [HttpGet("not-found")]
        public ActionResult<Usuario> GetNotFound()
        {
            var objeto = _context.Usuarios.Find(-1);
            if(objeto == null) return NotFound(new ApiErrorResponse(404));
         
            return objeto;
        }

        
        [HttpGet("paseador-not-found")]
        public ActionResult<Paseador> GetProfesorNotFound()
        {
            var profesor = _context.Paseadors.Find(-1);
            if (profesor == null) return NotFound(new ApiErrorResponse(404));
           
            return profesor;
        }

       
        [HttpGet("server-error")]
        public ActionResult<string> GetServerError()
        {
            var objeto = _context.Usuarios.Find(-1);
            var objetoString = objeto.ToString();
            return objetoString;
        }

        
        [HttpGet("bad-request")]
        public ActionResult<string> GetBadRequest()
        {
            return BadRequest(new ApiErrorResponse(400));
        }


    }
}
