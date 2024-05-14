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
            Session["SessionStatus"] = true;

            var data = _ciudadRepository.GetData();
            
            return View(data);
        }

        public ActionResult Create()
        {
            
            return View();
        }

        [HttpPost]
        public ActionResult Create(Ciudad  model)
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
        public ActionResult Edit(int id)
        {
            var data = _ciudadRepository.GetDataById(id);
            if (data == null)
            {
                return HttpNotFound();
            }
            return View(data);
        }

        [HttpPost]
        public ActionResult Edit(Ciudad usuario)
        {
            if (ModelState.IsValid)
            {
                _ciudadRepository.UpdateUsuario(usuario);
                return RedirectToAction("Index");
            }
            return View(usuario);
        }

        public ActionResult Details(int id)
        {
            var usuario = _ciudadRepository.GetDataById(id);
            if (usuario == null)
            {
                return HttpNotFound(); // Devuelve un error 404 si no se encuentra el usuario
            }
            return View(usuario);
        }

        public ActionResult Delete(int id)
        {
            var usuario = _ciudadRepository.GetDataById(id);
            if (usuario == null)
            {
                return HttpNotFound(); // Devuelve un error 404 si no se encuentra el usuario
            }
            return View(usuario);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _ciudadRepository.Delete(id);
            return RedirectToAction("Index");
        }
    }
}