//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VentasProyect.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class t_detalle_venta
    {
        public int det_id { get; set; }
        public Nullable<int> ven_id { get; set; }
        public Nullable<int> pro_id { get; set; }
        public Nullable<int> det_cantidad { get; set; }
        public string det_productos_vendidos { get; set; }
        public Nullable<int> det_valor_total { get; set; }
    
        public virtual t_producto t_producto { get; set; }
        public virtual t_venta t_venta { get; set; }
    }
}