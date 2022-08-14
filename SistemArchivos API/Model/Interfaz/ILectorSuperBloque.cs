namespace SistemArchivos_API.Model.Interfaz
{
    public interface ILectorSuperBloque
    {
        public Espacio<Bloque>[] GetBloques();
        public Espacio<INODO>[] GetNodos();
    }
}
