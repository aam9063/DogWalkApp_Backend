namespace API.Errores
{
    // Esta clase hereda de ApiErrorResponse y se utiliza para manejar excepciones en la API.
    public class ApiException : ApiErrorResponse
    {
        // En el constructor, se inicializan StatusCode, Mensaje y Detalle.
        public ApiException(int statusCode, string mensaje = null, string detalle = null) : base(statusCode, mensaje)
        {
            Detalle = detalle;
        }

        // Detalle es un detalle adicional del error.
        public string Detalle { get; set; }
    }

}