using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VentasProyect.Models.Persona;
using VentasProyect.Repository;

namespace VentasProyect.Controllers
{
    public class PersonaController : Controller
    {
        PersonaRepository _personaRepository = new PersonaRepository();
        CiudadRepository _ciudadRepository = new CiudadRepository();
        // GET: Persona
        public ActionResult Index(string type)
        {
            Session["SessionStatus"] = true;

            ViewBag.Type = type;

            var data = _personaRepository.GetData(type);

            return View(data);
        }

        public ActionResult Create()
        {
            ViewBag.Ciudades = _ciudadRepository.GetSelectCiudades();
            return View();
        }

        [HttpPost]
        public ActionResult Create(Persona model)
        {
            if (ModelState.IsValid)
            {


                // Lógica para guardar el nuevo usuario en la base de datos
                _personaRepository.Create(model);

                // Redirecciona al usuario a alguna página de confirmación o a la lista de usuarios
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public ActionResult Edit(int id)
        {
            var data = _personaRepository.GetDataById(id);
            if (data == null)
            {
                return HttpNotFound();
            }
            return View(data);
        }

        [HttpPost]
        public ActionResult Edit(Persona data)
        {
            if (ModelState.IsValid)
            {
                _personaRepository.Update(data);
                return RedirectToAction("Index");
            }
            return View(data);
        }

        public ActionResult Details(int id)
        {
            var usuario = _personaRepository.GetDataById(id);
            if (usuario == null)
            {
                return HttpNotFound(); // Devuelve un error 404 si no se encuentra el usuario
            }
            return View(usuario);
        }

        public ActionResult Delete(int id)
        {
            var usuario = _personaRepository.GetDataById(id);
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
            _personaRepository.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
