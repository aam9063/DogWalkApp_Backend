namespace API.Errores
{
    // Esta clase es un modelo de respuesta personalizado para manejar errores en la API.
    public class ApiErrorResponse
    {
        // En el constructor, se inicializan StatusCode y Mensaje.
        public ApiErrorResponse(int statusCode, string mensaje = null)
        {
            StatusCode = statusCode;
            Mensaje = mensaje ?? GetMensajeStatusCode(statusCode);
        }

        // StatusCode es el código de estado HTTP.
        public int StatusCode { get; set; }
        // Mensaje es el mensaje de error personalizado.
        public string Mensaje { get; set; }

        // GetMensajeStatusCode() devuelve un mensaje de error personalizado basado en el código de estado HTTP.
        private string GetMensajeStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "Se ha realizado una solicitud no válida",
                401 => "No estás autorizado para este recurso",
                404 => "Recurso No encontrado",
                500 => "Error interno del Servidor",
                _ => null
            };
        }
    }


}
