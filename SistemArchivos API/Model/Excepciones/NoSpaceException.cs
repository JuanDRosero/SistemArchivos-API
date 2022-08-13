namespace SistemArchivos_API.Model.Excepciones
{
    public class NoSpaceException: Exception
    {
        public NoSpaceException(String mensaje): base("Error "+mensaje)
        {
            //Excepcion utilizada para mostrar que no hay suficiente número de INODOS o BLoques
        }
    }
}
