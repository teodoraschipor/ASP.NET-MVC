﻿using MusicApp.Data;
using MusicApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MusicApp.Controllers
{
    public class SongsController : Controller
    {
        private readonly LibraryContext libraryContext = new LibraryContext(); // utilizam context-ul pentru a putea opera cu baza de date
        // GET: /Songs/Index
        [HttpGet]
        public ActionResult Index() // listam toate piesele din tabelul songs
        {
            var songs = libraryContext.Songs.ToList(); // luam piesele intr-o variabila si le listam cu tolist. 
                                                       // variabila de tip lista
          //  libraryContext.Songs.Where(x => x.ReleaseYear == DateTime.Now.Year).ToList(); //filtrare dupa an
          //  libraryContext.Songs.Find(5); //filtrare dupa id
          //  libraryContext.Songs.Skip(50).Take(10).ToList(); // paginare -- sar peste primele 5 pagini cu cate 10 randuri pe pagina...
                                                            // si imi arata urmatoarele 10 randuri(cu id-urile intre 51 si 60)
            

            return View(songs); // dam mai departe view-ului
        }

        //Get: /Songs/Details/{id}
        [HttpGet]
        public ActionResult Details(int id)
        {
            var song = libraryContext.Songs.Find(id);
            if(song == null) // daca nu exista cartea cu id-ul dat => eroare:
            {
                return HttpNotFound();
            }

            return View(song);
        }

        //Get: /Songs/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        //POST: /Songs/Create
        [HttpPost]
        public ActionResult Create(Song song)
        {
            if (ModelState.IsValid) // verif. daca modelul este valid
            {
                try
                {
                    libraryContext.Songs.Add(song); // face un INSERT in baza de date. Aici e entityFramework
                                                    // entityFramework are grija sa nu duplicam date, sa nu apara conflicte
                    libraryContext.SaveChanges(); // echivalent cu un COMMIT
                    return RedirectToAction("Index", "Songs"); //navigam intre view-uri. Dupa ce am adaugat=> ma trimite la Songs.
                }
                catch(Exception e)
                {
                    return Json(new { error = e.Message }, JsonRequestBehavior.AllowGet);
                }
                
            }
            return View(song); //daca sunt erori...puse in view create
        }

        //GET: /Songs/Update/{id}
        [HttpGet]
        public ActionResult Update(int id)
        {
            var song = libraryContext.Songs.Find(id);
            if(song == null)
            {
                return HttpNotFound();
            }


            return View(song);
        }

        //POST: /Songs/Update 
        [HttpPost]
        public ActionResult Update(Song song)
        {
            if (ModelState.IsValid) // verif. daca modelul este valid
            {
                try
                {
                    var oldSong = libraryContext.Songs.Find(song.Id); // iau piesa veche (o gasesc in functie de id)
                    
                    if(oldSong == null) // daca s-a schimbat id-ul din view => nu mai gaseste cartea:
                    {
                        return HttpNotFound();
                    }

                    oldSong.Title = song.Title;
                    oldSong.ArtistName = song.ArtistName;
                    oldSong.ReleaseYear = song.ReleaseYear;
                    oldSong.Purchased = song.Purchased;

                    // pana acum am actualizat
                    // => trebuie sa publicam modificarile:

                    TryUpdateModel(oldSong);
                    libraryContext.SaveChanges();

                    return RedirectToAction("Index", "Songs"); //navigam intre view-uri. Dupa ce am adaugat=> ma trimite la Songs.
                }
                catch (Exception e)
                {
                    return Json(new { error = e.Message }, JsonRequestBehavior.AllowGet);
                }

            }
            return View(song); //daca sunt erori...puse in view create
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
            var song = libraryContext.Songs.Find(id);

            if(song == null)
            {
                return HttpNotFound();
            }
            libraryContext.Songs.Remove(song);
            libraryContext.SaveChanges();

            return RedirectToAction("Index"); 
            // putem scrie RedirectToAction DOAR cu actiunea (FARA controller) daca vrem sa ne intoarcem in acelasi controller
        }
    }
}