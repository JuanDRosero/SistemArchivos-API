namespace SistemArchivos_API.Model.Excepciones
{
    public class NoFoundException: Exception
    {
        public NoFoundException(String mensaje): base("Error: "+ mensaje)
        {
              // Clase para explicar que no hay INODOS o Bloques disponibles
        }
    }
}
