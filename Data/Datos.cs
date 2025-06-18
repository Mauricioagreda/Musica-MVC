using Musica_MVC.Models;

namespace Musica_MVC.Data
{
    public class Datos
    {
        public static List<Artista> Artistas { get; set; } = new List<Artista>();
        public static List<Album> Albumes { get; set; } = new List<Album>();
        public static List<Cancion> Canciones { get; set; } = new List<Cancion>();
    }
}
