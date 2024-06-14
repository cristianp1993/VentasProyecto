using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VentasProyect.Models.Reportes;
using VentasProyect.Repository;

namespace VentasProyect.Controllers
{
    public class ReportesController : Controller
    {
        ReportesRepository _reportesRepository = new ReportesRepository();

        // GET: Reportes
        public ActionResult Index()
        {
            return View();
        }



    }
}
