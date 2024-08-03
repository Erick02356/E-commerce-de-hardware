using MVCtemplate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCtemplate.Controllers
{
    public class HomeController : Controller
    {
        private parcialEntities4 db = new parcialEntities4();

        public ActionResult Index()
        {
            var productos = db.Producto.ToList();
            return View(productos);
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