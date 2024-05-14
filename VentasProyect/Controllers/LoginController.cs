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
            Session["SessionStatus"] = false;
            if (result)
            {
                Session["Email"] = user;
                Session["SessionStatus"] = true;
            }
            else
            {
                Session["SessionStatus"] = false;
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