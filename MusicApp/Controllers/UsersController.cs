using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MusicApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MusicApp.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();

        // GET: /Users/Index
        public ActionResult Index()
        {
            // Luam toti users din baza de date
            var users = _context.Users.OrderBy(x => x.Email).ToList();
            ViewBag.Users = users;
            return View();
        }

        //GET: /Users/details/{id}
        public ActionResult Details(string id)
        {

            if (!String.IsNullOrEmpty(id)) // daca nu e null
            {
                // luam user-ul cu id-ul respectiv din context
                ApplicationUser user = _context
                    .Users
                    .Include(x => x.Roles)
                    .FirstOrDefault(x => x.Id.Equals(id));
                // daca acest user nu exista:
                if (user == null)
                {
                    return HttpNotFound();
                }

                // returnam rolul lui:
                var roleName = _context.Roles.Find(user.Roles.First().RoleId).Name;
                ViewData["roleName"] = roleName;

                return View(user);
            }

            // daca id-ul este null sau este empty:
            return HttpNotFound();
        }

        // GET: users/update/{id}
        public ActionResult Update(string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                return HttpNotFound();
            }

            UserViewModel model = new UserViewModel();

            var user = _context.Users.Find(id);

            if (user == null)
            {
                return HttpNotFound();
            }

            model.User = user;
            model.RoleName = _context.Roles.Find(user.Roles.First().RoleId).Name;

            return View(model);
        }

        // POST: users/update
        [HttpPost]
        public ActionResult Update(string id, UserViewModel model)
        {
            ApplicationUser user = _context.Users.Find(id);

            if (user == null)
            {
                return HttpNotFound();
            }

            try
            {
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_context));

                if (TryUpdateModel(user))
                {
                    var roles = _context.Roles.ToList();

                    foreach (var role in roles)
                    {
                        userManager.RemoveFromRole(user.Id, role.Name);
                    }

                    userManager.AddToRole(user.Id, model.RoleName);

                    _context.SaveChanges();

                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                return Json(new { error = e.Message }, JsonRequestBehavior.AllowGet);
            }

            return View(model);
        }

    }


}
