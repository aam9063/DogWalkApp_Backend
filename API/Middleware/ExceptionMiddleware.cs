using API.Errores;
using System.Net;
using System.Text.Json;

namespace API.Middleware
{
    // Esta clase es un middleware personalizado para manejar excepciones.
    public class ExceptionMiddleware
    {
        // _next es el siguiente middleware en la cadena de middleware.
        private readonly RequestDelegate _next;
        // _logger es una instancia de ILogger para registrar mensajes.
        private readonly ILogger<ExceptionMiddleware> _logger;
        // _env es una instancia de IHostEnvironment que proporciona información sobre el entorno de host.
        private readonly IHostEnvironment _env;

        // En el constructor, se inicializan _next, _logger y _env.
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        // InvokeAsync() es el método que se llama cuando el middleware procesa una solicitud HTTP.
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // Intenta ejecutar el siguiente middleware en la cadena.
                await _next(context);
            }
            catch (Exception ex)
            {
                // Si ocurre una excepción, registra el error y devuelve una respuesta HTTP con un código de estado 500 y detalles del error.
                _logger.LogError(ex, ex.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var response = _env.IsDevelopment()
                    ? new ApiException(context.Response.StatusCode, ex.Message, ex.StackTrace?.ToString())
                    : new ApiErrorResponse(context.Response.StatusCode);

                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

                var json = JsonSerializer.Serialize(response, options);

                await context.Response.WriteAsync(json);
            }
        }
    }

}