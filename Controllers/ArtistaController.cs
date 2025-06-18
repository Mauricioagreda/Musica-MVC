using Microsoft.AspNetCore.Mvc;
using Musica_MVC.Models;
using Musica_MVC.ViewModel;
using Musica_MVC.Data;
using System.Collections.Generic;
using System.Linq;

namespace Musica_MVC.Controllers
{
    public class ArtistaController : Controller
    {
        //private static List<Artista> artistas = Datos.Artistas;
        private static int id = 1;

        public IActionResult Lista()
        {
            return View(Datos.Artistas);
        }

        public IActionResult ListaAlbumes(int Id)
        {
            var artista = Datos.Artistas.FirstOrDefault(a => a.IdArtista == Id);
            var albumes = Datos.Albumes.Where(a => a.IdArtista == Id).ToList();
            if (artista == null || albumes == null)
            {
                return NotFound();
            }
            foreach(var album in albumes)
            {
                album.Artista = artista;
            }
            return View(albumes);
        }

        [HttpGet]
        public IActionResult Nuevo()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Nuevo(Artista artista)
        {
            artista.IdArtista = id++;
            Datos.Artistas.Add(artista);

            return RedirectToAction(nameof(Lista));
        }

        [HttpGet]
        public IActionResult Editar(int Id)
        {
            var artista = Datos.Artistas.FirstOrDefault(a => a.IdArtista == Id);

            if (artista == null)
                return NotFound();

            return View(artista);
        }

        [HttpPost]
        public IActionResult Editar(Artista artista)
        {
            var existente = Datos.Artistas.FirstOrDefault(a => a.IdArtista == artista.IdArtista);
            if (existente != null)
            {
                existente.Nombre = artista.Nombre;
                existente.NombreCompleto = artista.NombreCompleto;
            }
            return RedirectToAction(nameof(Lista));
        }

        [HttpGet]
        public IActionResult Eliminar(int Id)
        {
            var artista = Datos.Artistas.FirstOrDefault(a => a.IdArtista == Id);
            var albumes = Datos.Albumes.Where(a => a.IdArtista == Id);

            if (artista == null || albumes == null)
            {
                return NotFound();
            }
            foreach (var album in albumes)
            {
                Datos.Canciones.RemoveAll(a => a.IdAlbum == album.IdAlbum);
            }
            Datos.Albumes.RemoveAll(a => a.IdArtista == Id);
            Datos.Artistas.Remove(artista);

            return RedirectToAction(nameof(Lista));
        }
    }
}
