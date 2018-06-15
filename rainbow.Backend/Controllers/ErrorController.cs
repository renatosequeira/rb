using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace rainbow.Backend.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult _DuplicatedMobile()
        {
            return View();
        }
    }
}