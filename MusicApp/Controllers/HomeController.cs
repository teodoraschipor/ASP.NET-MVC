﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MusicApp.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index() 
        {
            return View();
        }

        public ActionResult Songs()
        {
            ViewBag.Message = "Your application songs page.";

            return View();
        }

    }
}