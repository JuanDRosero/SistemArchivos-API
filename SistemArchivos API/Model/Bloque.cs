namespace SistemArchivos_API.Model
{
    public class Bloque
    {
        public int Id { get; set; }
        public string NombreArchivo { get; set; }
        public int Tamaño { get; set; }
        public int TamañoOcupado { get; set; }
    }
}
