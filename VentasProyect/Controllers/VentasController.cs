using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VentasProyect.Models.Productos;
using VentasProyect.Models.Ventas;
using VentasProyect.Repository;

namespace VentasProyect.Controllers
{
    public class VentasController : Controller
    {
        ProductosRepository _productosRepository = new ProductosRepository();
        VentasRepository _ventasRepository = new VentasRepository();

        // GET: Ventas
        public ActionResult Index()
        {
            IEnumerable<Models.Ventas.Ventas> ventas = _ventasRepository.GetAll();

            if (!ventas.Any())
            {                
                
                ventas = new List<Models.Ventas.Ventas>(); 
            }

            return View(ventas);
        }

        public ActionResult Edit(int id)
        {
            var data = _ventasRepository.GetDataById(id);
            if (data == null)
            {
                return HttpNotFound();
            }
            return View(data);
        }

        [HttpPost]
        public ActionResult Edit(Ventas data)
        {
            if (ModelState.IsValid)
            {
                _ventasRepository.Update(data);
                return RedirectToAction("Index");
            }
            return View(data);
        }

        public ActionResult Details(int id)
        {
            var usuario = _ventasRepository.GetDataById(id);
            if (usuario == null)
            {
                return HttpNotFound(); // Devuelve un error 404 si no se encuentra el usuario
            }
            return View(usuario);
        }

        public ActionResult Delete(int id)
        {
            var usuario = _ventasRepository.GetDataById(id);
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
            _ventasRepository.Delete(id);
            return RedirectToAction("Index");
        }


        public ActionResult MakeSale(VentasProyect.Models.Productos.Productos dataProduct)
        {
            IEnumerable<Productos> productos = _productosRepository.GetProductos();
            return RedirectToAction("~/Views/Ventas/Sale.cshtml", productos);
        }
    }
}