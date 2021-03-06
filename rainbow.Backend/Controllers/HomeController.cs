﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using rainbow.Backend.Models;
using rainbow.Domain;

namespace rainbow.Backend.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            DataContext db = new DataContext();
            List<object> myModel = new List<object>();
            myModel.Add(db.Clientes.ToList());
            myModel.Add(db.Recomendacaos.ToList());
            myModel.Add(db.Demonstracaos.ToList());
            myModel.Add(db.EventosDeAgendas.ToList());
            return View(myModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
    
}