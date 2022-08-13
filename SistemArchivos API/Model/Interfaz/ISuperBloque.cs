namespace SistemArchivos_API.Model.Interfaz
{
    public interface ISuperBloque
    {
        public bool CrearArchivo(Archivo archivo, int padre);
        public bool CrearCarpeta(int padre, string interfaz);

    }
}
