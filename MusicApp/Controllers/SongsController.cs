﻿using MusicApp.Models;

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MusicApp.Controllers
{
    public class SongsController : Controller
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext(); // utilizam context-ul pentru a putea opera cu baza de date
        public SongsController()
        {
           
        }

        // GET: /Songs/Index
        [HttpGet]
        public ActionResult Index() // listam toate piesele din tabelul songs
        {
            // var songs = libraryContext.Songs.ToList(); // luam piesele intr-o variabila si le listam cu tolist. 
            // variabila de tip lista
            //  libraryContext.Songs.Where(x => x.ReleaseYear == DateTime.Now.Year).ToList(); //filtrare dupa an
            //  libraryContext.Songs.Find(5); //filtrare dupa id
            //  libraryContext.Songs.Skip(50).Take(10).ToList(); // paginare -- sar peste primele 5 pagini cu cate 10 randuri pe pagina...
            // si imi arata urmatoarele 10 randuri(cu id-urile intre 51 si 60)
            // libraryContext.Songs.Select(x, x=> Title); // cred ca iti afiseaza toate piesele + campul Title


            // return View(songs); // dam mai departe view-ului

              var songs = _context.Songs.Include(x=>x.Album).ToList();//include() face join intre tabele, aduce in piesa respectiva albumul
              ViewData["songs"] = songs;
             return View(songs);
            //return View();
        }


        //Get: /Songs/Details/{id}
           [HttpGet]
           public ActionResult Details(int id)
           {
               var song = _context.Songs.Include(x => x.Album).FirstOrDefault(x => x.Id == id);
               if (song == null) // daca nu exista cartea cu id-ul dat => eroare:
               {
                   return HttpNotFound();
               }

               return View(song);
           }

           //Get: /Songs/Create
           [HttpGet]
           public ActionResult Create()
           {
               // pentru DROPDOWN
               var albums = _context.Albums.Select(x => new
               {
                   AlbumId = x.id,
                   AlbumName = x.Title
               }).ToList();


               // am introdus un album din dropdown si adaugam in viewbag (=lista de albume) care ajunge in view => nu o sa fie null => nu ne va da eroare(merge)
               ViewBag.Albums = new SelectList(albums, "AlbumId", "AlbumName");

               return View();
           }

           //POST: /Songs/Create
           [HttpPost]
           public ActionResult Create(Song song)
           {
           
            if (ModelState.IsValid) // verif. daca modelul este valid
               {
                     _context.Songs.Add(song); // face un INSERT in baza de date. Aici e entityFramework
                                                 // entityFramework are grija sa nu duplicam date, sa nu apara conflicte
                     _context.SaveChanges(); // echivalent cu un COMMIT

                     return RedirectToAction("Index", "Songs"); //navigam intre view-uri. Dupa ce am adaugat=> ma trimite la Songs.
               }

               // pentru DROPDOWN:
               var albums = _context.Albums.Select(x => new
               {
                   AlbumId = x.id,
                   AlbumName = x.Title
               }).ToList();

               ViewBag.Albums = new SelectList(albums, "AlbumId", "AlbumName");

               return View(song); //daca sunt erori...puse in view create
           }

           //GET: /Songs/Update/{id}
           [HttpGet]
           public ActionResult Update(int id)
           {
               var song = _context.Songs.Find(id);
              // var albums = libraryContext.Albums.Select(x => new
              // {
              //     AlbumId = x.id,
              //     AlbumName = x.Title
              // }).ToList();

   //            ViewBag.Albums = new SelectList(albums, "AlbumId", "AlbumName");
               if (song == null)
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
                       var oldSong = _context.Songs.Find(song.Id); // iau piesa veche (o gasesc in functie de id)

                       if(oldSong == null) // daca s-a schimbat id-ul din view => nu mai gaseste cartea:
                       {
                           return HttpNotFound();
                       }

                       oldSong.Title = song.Title;
                       oldSong.ArtistName = song.ArtistName;
                       oldSong.Purchased = song.Purchased;

                       // pana acum am actualizat
                       // => trebuie sa publicam modificarile:

                       TryUpdateModel(oldSong);
                    _context.SaveChanges();

                       return RedirectToAction("Index", "Songs"); //navigam intre view-uri. Dupa ce am adaugat=> ma trimite la Index.
                   }
                   catch (Exception e)
                   {
                       return Json(new { error = e.Message }, JsonRequestBehavior.AllowGet);
                   }
                 //  var albums = libraryContext.Albums.Select(x => new
                 //  {
                 //      AlbumId = x.id,
                 //      AlbumName = x.Title
                 //  }).ToList();

                //   ViewBag.Albums = new SelectList(albums, "AlbumId", "AlbumName");
               }
               return View(song); //daca sunt erori...puse in view create
           }

           //POST: /Songs/Delete/{id}
           [HttpPost]
           public ActionResult Delete(int id) 
           {

               // 1. verificare existenta obiectului(find/where)
                //2. stergerea efectiva
               // 3. commit
               // 4. return to index

               var song = _context.Songs.Find(id);

               if(song == null)
               {
                   return HttpNotFound();
               }
            _context.Songs.Remove(song);
            _context.SaveChanges();

               return RedirectToAction("Index"); 
               // putem scrie RedirectToAction DOAR cu actiunea (FARA controller) daca vrem sa ne intoarcem in acelasi controller
           }
    
    }
}