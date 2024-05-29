namespace API.Errores
{
    // Esta clase hereda de ApiErrorResponse y se utiliza para manejar errores de validación en la API.
    public class ApiValidacionErrorResponse : ApiErrorResponse
    {
        // En el constructor, se inicializa StatusCode con 400, que es el código de estado HTTP para una solicitud incorrecta.
        public ApiValidacionErrorResponse() : base(400)
        {

        }

        // Errores es una lista de errores de validación.
        public IEnumerable<string> Errores { get; set; }
    }


}
