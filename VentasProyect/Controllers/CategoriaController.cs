using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VentasProyect.Models.Categoria;
using VentasProyect.Repository;

namespace VentasProyect.Controllers
{
    public class CategoriaController : Controller
    {
        CategoriaRepository _categoriaRepository = new CategoriaRepository();
        // GET: Categoria
        public ActionResult Index()
        {
            IEnumerable<Models.Categoria.Categoria> data = _categoriaRepository.GetData();
            return View(data);
        }
        public ActionResult Create()
        {
            
            return View();
        }

        [HttpPost]
        public ActionResult Create(Categoria model)
        {
            if (ModelState.IsValid)
            {

                // Lógica para guardar el nuevo usuario en la base de datos
                _categoriaRepository.Create(model);

                // Redirecciona al usuario a alguna página de confirmación o a la lista de usuarios
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public ActionResult Edit(int id)
        {
            var data = _categoriaRepository.GetDataById(id);
            if (data == null)
            {
                return HttpNotFound();
            }
            return View(data);
        }

        [HttpPost]
        public ActionResult Edit(Categoria usuario)
        {
            if (ModelState.IsValid)
            {
                _categoriaRepository.Update(usuario);
                return RedirectToAction("Index");
            }
            return View(usuario);
        }

        public ActionResult Details(int id)
        {
            var usuario = _categoriaRepository.GetDataById(id);
            if (usuario == null)
            {
                return HttpNotFound(); // Devuelve un error 404 si no se encuentra el usuario
            }
            return View(usuario);
        }

        public ActionResult Delete(int id)
        {
            var usuario = _categoriaRepository.GetDataById(id);
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
            _categoriaRepository.Delete(id);
            return RedirectToAction("Index");
        }
    }
}