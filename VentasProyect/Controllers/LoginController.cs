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
            
            bool loggedIn = Session["SessionStatus"] != null && (bool)Session["SessionStatus"];

            
            ViewBag.SessionStatus = loggedIn;
            return View();
        }

        [HttpPost]
        public bool ValidateUser(string user, string password)
        {

            var result = loginRepository.ValidateLogin(user,password);

            if (result)
            {
                Session["Email"] = user;
                Session["SessionStatus"] = result;
            }

            return result;

        }

        public bool CloseSesion()
        {
            Session["SessionStatus"] = false;
            ViewBag.SessionStatus = Session["SessionStatus"];

            return false;
        }
    }
}