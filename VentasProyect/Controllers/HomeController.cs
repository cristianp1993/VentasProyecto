using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VentasProyect.Repository.HomeRepository;
using VentasProyect.Repository.LoginRepository;

namespace VentasProyect.Controllers
{
    public class HomeController : Controller
    {
        
        public ActionResult Index()
        {
            // Obtener el valor de la variable de sesión SessionStatus
            bool loggedIn = Session["SessionStatus"] != null && (bool)Session["SessionStatus"];

            // Asignar el valor a una variable de vista (view bag)
            ViewBag.SessionStatus = loggedIn;           
            
            return View();
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