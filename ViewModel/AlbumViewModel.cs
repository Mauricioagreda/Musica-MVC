using Microsoft.AspNetCore.Mvc.Rendering;
using Musica_MVC.Models;

namespace Musica_MVC.ViewModel
{
    public class AlbumViewModel
    {
        public Album AlbumVM { get; set; }
        public List<SelectListItem> Artistas { get; set; }
    }
}
