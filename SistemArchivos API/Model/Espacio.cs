namespace SistemArchivos_API.Model
{
    public class Espacio<T>
    {
        public int Id;
        public T elemento;
        public string tipo;
        public bool libre = true;
    }
}
