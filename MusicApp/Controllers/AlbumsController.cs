﻿using MusicApp.Data;
using MusicApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MusicApp.Controllers
{
    public class AlbumsController : Controller
    {
        private readonly LibraryContext libraryContext = new LibraryContext(); // utilizam context-ul pentru a putea opera cu baza de date
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
                    return RedirectToAction("Index", "Albums"); //navigam intre view-uri. Dupa ce am adaugat=> ma trimite la Songs.
                }
                catch (Exception e)
                {
                    return Json(new { error = e.Message }, JsonRequestBehavior.AllowGet);
                }

            }
            return View(album); //daca sunt erori...puse in view create
        }

        //GET: /Albums/Update/{id}
        [HttpGet]
        public ActionResult Update(int id)
        {
            var album = libraryContext.Albums.Find(id);
            if (album == null)
            {
                return HttpNotFound();
            }


            return View(album);
        }

        //POST: /Albums/Update 
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

            }
            return View(album); //daca sunt erori...puse in view create
        }

        //POST: /Albums/Delete/{id}
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
    }
}