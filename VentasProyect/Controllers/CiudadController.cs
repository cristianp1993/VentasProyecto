using System.Web.Mvc;
using VentasProyect.Models.Ciudad;
using VentasProyect.Repository;

namespace VentasProyect.Controllers
{
    public class CiudadController : Controller
    {
        public CiudadRepository _ciudadRepository = new CiudadRepository();
        // GET: Ciudad
        public ActionResult Index()
        {
            try
            {
                Session["SessionStatus"] = true;

                var data = _ciudadRepository.GetData();

                return View(data);
            }
            catch (System.Exception)
            {

                return View("Index", "Error");
            }
        }

        public ActionResult Create()
        {
            try
            {
                return View();

            }
            catch (System.Exception)
            {

                return View("Index", "Error");
            }
        }

        [HttpPost]
        public ActionResult Create(Ciudad model)
        {
            try
            {
                if (ModelState.IsValid)
                {


                    // Lógica para guardar el nuevo usuario en la base de datos
                    _ciudadRepository.CreateCiudad(model);

                    // Redirecciona al usuario a alguna página de confirmación o a la lista de usuarios
                    return RedirectToAction("Index");
                }
                return View(model);
            }
            catch (System.Exception)
            {

                return View("Index", "Error");
            }

        }
        public ActionResult Edit(int id)
        {
            try
            {
                var data = _ciudadRepository.GetDataById(id);
                if (data == null)
                {
                    return HttpNotFound();
                }
                return View(data);
            }
            catch (System.Exception)
            {

                return View("Index", "Error");
            }
        }

        [HttpPost]
        public ActionResult Edit(Ciudad usuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _ciudadRepository.UpdateUsuario(usuario);
                    return RedirectToAction("Index");
                }

                return View(usuario);
            }


            catch (System.Exception)
            {

                return View("Index", "Error");
            }
        }

        public ActionResult Details(int id)
        {
            try
            {
                var usuario = _ciudadRepository.GetDataById(id);
                if (usuario == null)
                {
                    return HttpNotFound(); // Devuelve un error 404 si no se encuentra el usuario
                }
                return View(usuario);
            }
            catch (System.Exception)
            {

                return View("Index", "Error");
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                var usuario = _ciudadRepository.GetDataById(id);
                if (usuario == null)
                {
                    return HttpNotFound(); // Devuelve un error 404 si no se encuentra el usuario
                }
                return View(usuario);
            }
            catch (System.Exception)
            {

                return View("Index", "Error");
            }

        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                _ciudadRepository.Delete(id);
                return RedirectToAction("Index");
            }
            catch (System.Exception)
            {

                return View("Index", "Error");
            }
        }
    }
}