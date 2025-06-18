namespace Musica_MVC.Models
{
    public class Album
    {
        public int IdAlbum { get; set; }
        public string Nombre { get; set; }
        public int IdArtista { get; set; }
        public Artista Artista { get; set; }

        //public List<Cancion> canciones { get; set; }
        //public Album()
        //{
        //    canciones = new List<Cancion>();
        //}
    }
}
