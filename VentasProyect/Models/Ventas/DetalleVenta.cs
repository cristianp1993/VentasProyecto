using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VentasProyect.Models.Ventas
{
    public class DetalleVenta
    {
        public int ven_id { get; set; }
        public int pro_id { get; set; }

        [Display(Name = "Cantidad")]
        public int det_cantidad { get; set; }

        [Display(Name = "Valor Total")]
        public decimal det_valor_total { get; set; }

        [Display(Name = "Articulo")]
        public string pro_nombre { get; set; }
        public int pro_valor_unitario { get; set; }
    }
}