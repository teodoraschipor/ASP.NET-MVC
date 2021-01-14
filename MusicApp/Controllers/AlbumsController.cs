
using MusicApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MusicApp.Controllers
{
    public class AlbumsController : Controller
    {
        private readonly ApplicationDbContext libraryContext = new ApplicationDbContext(); // utilizam context-ul pentru a putea opera cu baza de date
        // GET: /Albums/Index
        [HttpGet]
        public ActionResult Index()
        {
            var albums = libraryContext.Albums.ToList();

            return View(albums);
        }
        //Get: /Albums/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        //Get: /Albums/Details/{id}
        [HttpGet]
        public ActionResult Details(int id)
        {
              var album = libraryContext.Albums.Find(id);
          //  var album = libraryContext.Albums.FirstOrDefault(x => x.id == id); // sa ne asiguram ca e unic(si in cazul asta id e unic pt ca e cheie primara).
            //default == returneaza null daca nu gaseste nimic

            if (album == null) // daca nu exista cartea cu id-ul dat => eroare:
            {
                return HttpNotFound();
            }

            return View(album);
        }

        //POST: /Albums/Create
        [HttpPost]
        public ActionResult Create(Album album)
        {
            if (ModelState.IsValid) // verif. daca modelul este valid
            {
                try
                {
                    libraryContext.Albums.Add(album); // face un INSERT in baza de date. Aici e entityFramework
                                                    // entityFramework are grija sa nu duplicam date, sa nu apara conflicte
                    libraryContext.SaveChanges(); // echivalent cu un COMMIT
                    
                    return RedirectToAction("Create", "Songs"); //navigam intre view-uri. Dupa ce am adaugat=> ma trimite la Songs.
                }
                catch (Exception e)
                {
                    return Json(new { error = e.Message }, JsonRequestBehavior.AllowGet);
                }

            }
            return View(album); //daca sunt erori...puse in view create

        
        }

        //GET: /Songs/Update/{id}
        [HttpGet]
        public ActionResult Update(int id)
        {
            var album = libraryContext.Albums.Find(id);
            /* var albums = libraryContext.Albums.Select(x => new
             {
                 AlbumId = x.id,
                 AlbumName = x.Title
             }).ToList();

             ViewBag.Albums = new SelectList(albums, "AlbumId", "AlbumName");*/
            if (album == null)
            {
                return HttpNotFound();
            }


            return View(album);
        }

        //POST: /Songs/Update 
        [HttpPost]
        public ActionResult Update(Album album)
        {
            if (ModelState.IsValid) // verif. daca modelul este valid
            {
                try
                {
                    var oldAlbum = libraryContext.Albums.Find(album.id); // iau piesa veche (o gasesc in functie de id)

                    if (oldAlbum == null) // daca s-a schimbat id-ul din view => nu mai gaseste cartea:
                    {
                        return HttpNotFound();
                    }

                    oldAlbum.Title = album.Title;
                    oldAlbum.ArtistName = album.ArtistName;
                    oldAlbum.ReleaseYear = album.ReleaseYear;
                    oldAlbum.Songs = album.Songs;
                    // pana acum am actualizat
                    // => trebuie sa publicam modificarile:

                    TryUpdateModel(oldAlbum);
                    libraryContext.SaveChanges();

                    return RedirectToAction("Index", "Albums"); //navigam intre view-uri. Dupa ce am adaugat=> ma trimite la Index.
                }
                catch (Exception e)
                {
                    return Json(new { error = e.Message }, JsonRequestBehavior.AllowGet);
                }
                /*  var albums = libraryContext.Albums.Select(x => new
                  {
                      AlbumId = x.id,
                      AlbumName = x.Title
                  }).ToList();

                  ViewBag.Albums = new SelectList(albums, "AlbumId", "AlbumName");*/
            }
            return View(album); //daca sunt erori...puse in view create
        }

        //POST: /Songs/Delete/{id}
        [HttpPost]
        public ActionResult Delete(int id)
        {
            /*
             1. verificare existenta obiectului(find/where)
             2. stergerea efectiva
             3. commit
             4. return to index
             */
            var album = libraryContext.Albums.Find(id);

            if (album == null)
            {
                return HttpNotFound();
            }
            libraryContext.Albums.Remove(album);
            libraryContext.SaveChanges();

            return RedirectToAction("Index");
            // putem scrie RedirectToAction DOAR cu actiunea (FARA controller) daca vrem sa ne intoarcem in acelasi controller
        }
        public ActionResult DataTables()
        {
            return View();
        }


        public ActionResult UpdateSongs(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Album album = libraryContext.Albums.Include(x => x.Songs).FirstOrDefault(x => x.id == id);
            if (album == null)
            {
                return HttpNotFound();
            }

            ViewBag.SongId = new SelectList(libraryContext.Songs, "Id", "Title");

            return View(album);
        }
        [HttpPost]
        public ActionResult Add(int? id, int? songId)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Album album = libraryContext.Albums.Include(x => x.Songs).FirstOrDefault(x => x.id == id);
            if (album == null)
            {
                return HttpNotFound();
            }

            if (songId == null)
            {
                return HttpNotFound();
            }

            Song song = libraryContext.Songs.Find(songId);
            if (song == null)
            {
                return HttpNotFound();
            }

            album.Songs.Add(song);
            libraryContext.SaveChanges();

            ViewBag.SongId = new SelectList(libraryContext.Songs, "Id", "Title");

            return View("UpdateSongs", album);
        }

        [HttpPost]
        public ActionResult Remove(int? id, int? songId)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Album album = libraryContext.Albums.Include(x => x.Songs).FirstOrDefault(x => x.id == id);
            if (album == null)
            {
                return HttpNotFound();
            }

            if (songId == null)
            {
                return HttpNotFound();
            }

            Song song = libraryContext.Songs.Find(songId);
            if (song == null)
            {
                return HttpNotFound();
            }
            libraryContext.Songs.Remove(song);
            libraryContext.SaveChanges();
            album.Songs.Remove(song);
            libraryContext.SaveChanges();


            ViewBag.SongId = new SelectList(libraryContext.Songs, "Id", "Title");

            return View("UpdateSongs", album);
        }

    }
}