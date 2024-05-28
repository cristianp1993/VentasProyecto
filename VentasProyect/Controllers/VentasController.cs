using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VentasProyect.Repository;

namespace VentasProyect.Controllers
{
    public class VentasController : Controller
    {

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
    }
}