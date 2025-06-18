using Microsoft.AspNetCore.Mvc.Rendering;
using Musica_MVC.Models;


namespace Musica_MVC.ViewModel
{
    public class CancionViewModel
    {
        public Cancion CancionVM { get; set; }
        public List<SelectListItem> Albumes { get; set; }
    }
}
