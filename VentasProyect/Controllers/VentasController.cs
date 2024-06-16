using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
        CiudadRepository _ciudadRepository = new CiudadRepository();
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
            var data = _ventasRepository.GetDataById(id);
            if (data == null)
            {
                return HttpNotFound(); // Devuelve un error 404 si no se encuentra el usuario
            }
            return View(data);
        }

        public ActionResult Delete(int id)
        {
            var data = _ventasRepository.GetDataById(id);
            if (data == null)
            {
                return HttpNotFound(); // Devuelve un error 404 si no se encuentra el usuario
            }
            return View(data);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _ventasRepository.Delete(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult MakeSale(DataProductRequest request)
        {
            try
            {
                if (request.DataProduct == null)
                {
                    return Json(new { success = false, message = "No data received" });
                }

                var arrayProductsModel = JsonConvert.DeserializeObject<Productos[]>(request.DataProduct);
                
                IEnumerable<Productos> productos = arrayProductsModel.ToList();
               
                var productosJson = JsonConvert.SerializeObject(productos);

                // Almacenar los productos en la sesión en lugar de TempData
                Session["Products"] = productosJson;                

                // Devuelve una URL de redirección a la vista de venta junto con los productos
                return Json(new { success = true, productos = productosJson, redirectUrl = Url.Action("Sale", "Ventas") });
            }
            catch (Exception)
            {
                // Devuelve una URL de redirección a la vista de error
                return Json(new { success = false, redirectUrl = Url.Action("Index", "Error") });
            }
        }

        [HttpGet]
        public ActionResult Sale()
        {

            string productosJson = Session["Products"] as string; 

            if (!string.IsNullOrEmpty(productosJson))
            {
                ViewBag.Ciudades = _ciudadRepository.GetSelectCiudades();

                var productosList = JsonConvert.DeserializeObject<IEnumerable<Productos>>(productosJson);
                if (productosList.Any())
                {
                    // Los productos ya han sido obtenidos, mostrar la vista directamente
                    return View(productosList);
                }
            }

            return RedirectToAction("Index", "Error");
        }

        public class DataProductRequest
        {
            public string DataProduct { get; set; }
        }
        public ActionResult SaveSale(List<Productos> products)
        {

            try
            {
                int totalSale = 0;

                if (products.Count > 0)
                {
                    DateTime fechaActual = DateTime.Today;

                    Random rnd = new Random();
                    int transactionNumber = rnd.Next(100000000, 999999999);

                    foreach (var item in products)
                    {
                        totalSale += Convert.ToInt32(item.pro_cantidad) * item.pro_valor_unitario;
                    }

                    var newData = new Models.Ventas.Ventas
                    {
                        ven_id = 0,
                        per_id = 1,
                        usu_id = 1,
                        ven_fecha = fechaActual,
                        ven_metodo_pago = "CONTADO",
                        ven_total = totalSale,
                        ven_numero_transaccion = transactionNumber
                    };

                    _ventasRepository.Create(newData, products);

                }
                                
                return Json(new { success = true, message = "Venta guardada exitosamente." });
            }
            catch (Exception ex)
            {

                return RedirectToAction("Index", "Error");
            }
           
        }
    }
}