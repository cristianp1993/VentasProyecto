using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VentasProyect.Models.Ventas
{
    public class DetalleVenta
    {
        public int ven_id { get; set; }
        public int pro_id { get; set; }
        public int det_cantidad { get; set; }
        public decimal det_valor_total { get; set; }
        public string pro_nombre { get; set; }
    }
}