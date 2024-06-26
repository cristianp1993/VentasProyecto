﻿using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using VentasProyect.Models.Productos;
using VentasProyect.Repository;

namespace VentasProyect.Controllers
{
    public class HomeController : Controller
    {
        private readonly ProductosRepository _productosRepository = new ProductosRepository();
        private readonly CategoriaRepository _categoriaRepository = new CategoriaRepository();

        public ActionResult Index(string selectedCategories)
        {
            try
            {
                
                if (HttpContext.Session["SessionStatus"] == null)
                {
                    // La sesión existe, entonces obtén su valor y conviértelo a booleano
                    HttpContext.Session["SessionStatus"] = false;
                } 
                
                if (HttpContext.Session["Email"] == null)
                {
                    // La sesión existe, entonces obtén su valor y conviértelo a booleano
                    HttpContext.Session["Email"] = string.Empty;
                }
                
                IEnumerable<Productos> productos = _productosRepository.GetProductos(); // Este método ya filtra productos inactivos

                if (!string.IsNullOrEmpty(selectedCategories))
                {
                    var categories = selectedCategories.Split(',').Select(int.Parse).ToList();
                    productos = productos.Where(p => categories.Contains(int.Parse(p.cat_id)));
                }

                ViewBag.Categories = _categoriaRepository.GetSelectCategorias();

                return View(productos);
            }
            catch (System.Exception ex)
            {

                return View("Index", "Error");
            }

        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }
    }
}
