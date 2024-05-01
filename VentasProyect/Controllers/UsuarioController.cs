using System;
using System.Web.Mvc;
using VentasProyect.Repository;

namespace VentasProyect.Controllers
{
    public class UsuarioController : Controller
    {

        UsuarioRepository usuarioRepository = new UsuarioRepository();
        

        // GET: Usuario
        public ActionResult Index()
        {

            bool loggedIn = Session["SessionStatus"] != null && (bool)Session["SessionStatus"];


            ViewBag.SessionStatus = loggedIn;

            var usuarios = usuarioRepository.GetUsuarios();

            return View(usuarios);
        }

        //public ActionResult Create()
        //{
        //    View()
        //}

        //[HttpPost]
        //public ActionResult Create(Models.usuario.usuario)
        //{

        //}
    }
}