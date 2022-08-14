namespace SistemArchivos_API.Model.Excepciones
{
    public class NoPermissionException: Exception
    {
        public NoPermissionException(string menssage): base(menssage)
        {

        }
    }
}
