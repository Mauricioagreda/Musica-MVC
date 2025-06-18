namespace Musica_MVC.Models
{
    public class Cancion
    {
        public int IdCancion { get; set; }
        public string Nombre { get; set; }
        public double Duracion { get; set; }
        public int IdAlbum { get; set; }
        public Album Album { get; set; }
    }
}
