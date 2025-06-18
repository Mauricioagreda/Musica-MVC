using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Musica_MVC.Models;
using Musica_MVC.ViewModel;
using Musica_MVC.Data;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.SignalR.Protocol;

namespace Musica_MVC.Controllers
{
    public class AlbumController : Controller
    {
        //public static List<Artista> artistas = Datos.Artistas;
        //public static List<Album> albumes = Datos.Albumes;

        public IActionResult Lista()
        {
            return View(Datos.Albumes);
        }

        public IActionResult ListaCanciones(int Id)
        {
            var album = Datos.Albumes.FirstOrDefault(a => a.IdAlbum == Id);
            var canciones = Datos.Canciones.Where(a => a.IdAlbum == Id).ToList();

            if(album == null || canciones == null)
            {
                return NotFound();
            }
            foreach (var cancion in canciones)
            {
                cancion.Album = album;
            }

            return View(canciones);
        }

        [HttpGet]
        public IActionResult Nuevo()
        {
            var viewModel = new AlbumViewModel()
            {
                AlbumVM = new Album(),
                Artistas = Datos.Artistas.Select(a => new SelectListItem
                {
                    Value = a.IdArtista.ToString(),
                    Text = a.Nombre
                }).ToList()
            };

            return View(viewModel);
        }
        [HttpPost]
        public IActionResult Nuevo(AlbumViewModel viewModel)
        {
            var artista = Datos.Artistas.FirstOrDefault(a => a.IdArtista == viewModel.AlbumVM.IdArtista);
            if (artista != null)
            {
                viewModel.AlbumVM.Artista = artista;
            }

            viewModel.AlbumVM.IdAlbum = Datos.Albumes.Count > 0 ? Datos.Albumes.Max(a => a.IdAlbum) + 1 : 1;
            Datos.Albumes.Add(viewModel.AlbumVM);

            return RedirectToAction(nameof(Lista));
        }

        [HttpGet]
        public IActionResult Editar(int Id)
        {
            var album = Datos.Albumes.FirstOrDefault(a => a.IdAlbum == Id);
            if (album == null)
            {
                return NotFound();
            }
            var viewModel = new AlbumViewModel()
            {
                AlbumVM = album,
                Artistas = Datos.Artistas.Select(a => new SelectListItem
                {
                    Value = a.IdArtista.ToString(),
                    Text = a.Nombre
                }).ToList()
            };

            return View(viewModel);
        }
        [HttpPost]
        public IActionResult Editar(AlbumViewModel viewModel)
        {
            var existente = Datos.Albumes.FirstOrDefault(a => a.IdAlbum == viewModel.AlbumVM.IdAlbum);
            var seleccionado = Datos.Artistas.FirstOrDefault(a => a.IdArtista == viewModel.AlbumVM.IdArtista);

            if (existente != null && seleccionado != null)
            {
                existente.Nombre = viewModel.AlbumVM.Nombre;
                existente.Artista = seleccionado;
            }

            return RedirectToAction(nameof(Lista));
        }

        [HttpGet]
        public IActionResult Eliminar(int Id)
        {
            var album = Datos.Albumes.FirstOrDefault(a => a.IdAlbum == Id);
            if (album == null)
            {
                return NotFound();
            }

            Datos.Canciones.RemoveAll(a => a.IdAlbum == Id);
            Datos.Albumes.Remove(album);

            return RedirectToAction(nameof(Lista));
        }
    }
}
