using System;
using System.Collections.Generic;
using System.Linq;
using VentasProyect.Models;
using VentasProyect.Models.Reportes;

namespace VentasProyect.Repository
{
    public class ReportesRepository
    {
        private readonly VENTAS_DBEntities1 _dbContext;

        public ReportesRepository()
        {
            _dbContext = new VENTAS_DBEntities1();
        }

    }
}
