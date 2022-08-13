namespace SistemArchivos_API.Model
{
    public class INODO
    {
        public int ID { get; set; }
        public int Padre { get; set; }
        public string Nombre { get; set; }

        public string HoraCreacion {get; set;}
        public List<Puntero> Punteros { get;} = new List<Puntero>();
    }
}
