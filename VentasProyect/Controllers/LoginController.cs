using System;
using System.Web.Mvc;
using VentasProyect.Repository.LoginRepository;

namespace VentasProyect.Controllers
{
    public class LoginController : Controller
    {
        LoginRepository loginRepository = new LoginRepository();
        // GET: Login
        public ActionResult Index()
        {

            
            return View();
        }

        [HttpPost]
        public bool ValidateUser(string user, string password)
        {

            var result = loginRepository.ValidateLogin(user,password);
            HttpContext.Session["SessionStatus"] = false;
            if (result)
            {
                HttpContext.Session["Email"] = user;
                HttpContext.Session["SessionStatus"] = true;
            }
            else
            {
                HttpContext.Session["SessionStatus"] = false;
            }

            return result;

        }

      

        public ActionResult CloseSesion()
        {
            HttpContext.Session["SessionStatus"] = false;
            ViewBag.SessionStatus = HttpContext.Session["SessionStatus"];
            HttpContext.Session["Email"] = string.Empty;
            return View("Index");
        }
    }
}