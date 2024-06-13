using Newtonsoft.Json;
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

        
        public ActionResult MakeSale(string dataProduct)
        {
            if (dataProduct == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "No data received");
            }
            var arrayProductsModel = JsonConvert.DeserializeObject<Productos[]>(dataProduct);

            //IEnumerable<Productos> productos = _productosRepository.GetProductos();
            IEnumerable<Productos> productos = arrayProductsModel.ToList() ;
            //ViewBag.DataProduct = dataProduct;
            return View("~/Views/Ventas/Sale.cshtml", productos);
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
                        per_id = 7,
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

                throw;
            }
           
        }
    }
}