using Microsoft.AspNetCore.Mvc;
using Musica_MVC.Models;
using Musica_MVC.ViewModel;
using Musica_MVC.Data;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Musica_MVC.Controllers
{
    public class CancionController : Controller
    {
        //public static List<Album> albumes = Datos.Albumes;
        public IActionResult Lista()
        {
            return View(Datos.Canciones);
        }

        [HttpGet]
        public IActionResult Nuevo()
        {
            var viewModel = new CancionViewModel()
            {
                CancionVM = new Cancion(),
                Albumes = Datos.Albumes.Select(a => new SelectListItem
                {
                    Value = a.IdAlbum.ToString(),
                    Text = a.Nombre,
                }).ToList()
            };

            return View(viewModel);
        }
        [HttpPost]
        public IActionResult Nuevo(CancionViewModel viewModel)
        {
            var album = Datos.Albumes.FirstOrDefault(a => a.IdAlbum == viewModel.CancionVM.IdAlbum);
            if (album != null)
            {
                viewModel.CancionVM.Album = album;
            }
            viewModel.CancionVM.IdCancion = Datos.Canciones.Count > 0 ? Datos.Canciones.Max(a => a.IdCancion) + 1 : 1;
            Datos.Canciones.Add(viewModel.CancionVM);

            return RedirectToAction(nameof(Lista));
        }

        [HttpGet]
        public IActionResult Editar(int Id)
        {
            var cancion = Datos.Canciones.FirstOrDefault(a => a.IdCancion == Id);
            if (cancion == null)
            {
                return NotFound();
            }
            var viewModel = new CancionViewModel()
            {
                CancionVM = cancion,
                Albumes = Datos.Albumes.Select(a => new SelectListItem
                {
                    Value = a.IdAlbum.ToString(),
                    Text = a.Nombre
                }).ToList()
            };

            return View(viewModel);
        }
        [HttpPost]
        public IActionResult Editar(CancionViewModel viewModel)
        {
            var existente = Datos.Canciones.FirstOrDefault(a => a.IdCancion == viewModel.CancionVM.IdCancion);
            var seleccionado = Datos.Albumes.FirstOrDefault(a => a.IdAlbum == viewModel.CancionVM.IdAlbum);
            if (existente != null && seleccionado != null)
            {
                existente.Nombre = viewModel.CancionVM.Nombre;
                existente.Duracion = viewModel.CancionVM.Duracion;
                existente.Album = seleccionado;
            }

            return RedirectToAction(nameof(Lista));
        }

        [HttpGet]
        public IActionResult Eliminar(int Id)
        {
            var cancion = Datos.Canciones.FirstOrDefault(a => a.IdCancion == Id);
            if (cancion != null)
            {
                Datos.Canciones.Remove(cancion);
            }

            return RedirectToAction(nameof(Lista));
        }
    }
}
