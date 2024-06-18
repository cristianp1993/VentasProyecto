using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VentasProyect.Models.Ventas
{
    public class Ventas
    {
        public int ven_id { get; set; }
        public Nullable<int> per_id { get; set; }
        public string per_nombre { get; set; }
        public Nullable<int> usu_id { get; set; }
        public string usu_nombre { get; set; }
        public Nullable<System.DateTime> ven_fecha { get; set; }
        public string ven_metodo_pago { get; set; }
        public Nullable<long> ven_total { get; set; }
        public Nullable<long> ven_numero_transaccion { get; set; }
        public int ven_cedula { get; set; }

        [MaxLength(255)]
        public string ven_nombre { get; set; }

        
    }

    public class VentaConDetalle
    {
        public Ventas head { get; set; }
        public IEnumerable<DetalleVenta> detail { get; set; }
    }
}