using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using VentasProyect.Models.Productos;
using VentasProyect.Repository;

namespace VentasProyect.Controllers
{
    public class ProductosController : Controller
    {
        ProductosRepository _productosRepository = new ProductosRepository();
        CategoriaRepository _categoriaRepository = new CategoriaRepository();
        // GET: Productos

        public ActionResult Index()
        {
            IEnumerable<Models.Productos.Productos> data = _productosRepository.GetAllProductos();
            return View(data);
        }


        public ActionResult Create()
        {
            ViewBag.Categorias = _categoriaRepository.GetSelectCategorias();
            return View();
        }

        [HttpPost]
        public ActionResult Create(Productos model)
        {
            if (ModelState.IsValid) 
            {
                _productosRepository.CreateProduct(model);

                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult Edit(int id) 
        {
            ViewBag.Categorias = _categoriaRepository.GetSelectCategorias();
            var data = _productosRepository.GetDataById(id);
            if (data == null)
            {
                return HttpNotFound();
            }

            ViewBag.Estados = new List<SelectListItem>
            {
                new SelectListItem { Text = "Activo", Value = "Activo" },
                new SelectListItem { Text = "Inactivo", Value = "Inactivo" },
                new SelectListItem { Text = "Disponible", Value = "Disponible" },
                new SelectListItem { Text = "Agotado", Value = "Agotado" }
            };

            return View(data);
        }

        [HttpPost]
        public ActionResult Edit(Productos data)
        {
            if (ModelState.IsValid)
            {
                if (data.pro_stock == 0 && data.pro_estado == "Disponible")
                {
                    ModelState.AddModelError("pro_estado", "No puedes establecer el estado como 'Disponible' si el stock es cero.");
                    ViewBag.Categorias = _categoriaRepository.GetSelectCategorias();
                    ViewBag.Estados = new List<SelectListItem>
            {
                new SelectListItem { Text = "Activo", Value = "Activo" },
                new SelectListItem { Text = "Inactivo", Value = "Inactivo" },
                new SelectListItem { Text = "Disponible", Value = "Disponible", Disabled = true },
                new SelectListItem { Text = "Agotado", Value = "Agotado" }
            };
                    return View(data);
                }

                if (data.pro_stock > 0 && (data.pro_estado == "Agotado" || data.pro_estado == "Inactivo"))
                {
                    ModelState.AddModelError("pro_estado", "No puedes establecer el estado como 'Agotado' o 'Inactivo' si el stock es mayor que cero.");
                    ViewBag.Categorias = _categoriaRepository.GetSelectCategorias();
                    ViewBag.Estados = new List<SelectListItem>
            {
                new SelectListItem { Text = "Activo", Value = "Activo" },
                new SelectListItem { Text = "Inactivo", Value = "Inactivo" },
                new SelectListItem { Text = "Disponible", Value = "Disponible" },
                new SelectListItem { Text = "Agotado", Value = "Agotado", Disabled = true }
            };
                    return View(data);
                }

                _productosRepository.UpdateProduct(data);
                return RedirectToAction("Index");
            }

            ViewBag.Categorias = _categoriaRepository.GetSelectCategorias();

            // Asegúrate de volver a configurar los estados si hay un error de validación
            ViewBag.Estados = new List<SelectListItem>
            {
                new SelectListItem { Text = "Activo", Value = "Activo" },
                new SelectListItem { Text = "Inactivo", Value = "Inactivo" },
                new SelectListItem { Text = "Disponible", Value = "Disponible" },
                new SelectListItem { Text = "Agotado", Value = "Agotado" }
            };
            return View(data);
        }


        public ActionResult Details(int id)
        {
            var producto = _productosRepository.GetDataById(id);
            if (producto == null)
            {
                return HttpNotFound();
            }

            // Obtener el nombre de la categoría
            var categoria = _categoriaRepository.GetDataById(Convert.ToInt32(producto.cat_id));
            if (categoria != null)
            {
                producto.cat_nombre = categoria.cat_nombre;
            }

            return View(producto);
        }

    }
}