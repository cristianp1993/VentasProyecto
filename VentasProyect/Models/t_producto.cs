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
    
    public partial class t_producto
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public t_producto()
        {
            this.t_detalle_venta = new HashSet<t_detalle_venta>();
            this.t_pedido = new HashSet<t_pedido>();
        }
    
        public int pro_id { get; set; }
        public Nullable<int> cat_id { get; set; }
        public string pro_nombre { get; set; }
        public string pro_descripcion { get; set; }
        public Nullable<int> pro_valor_unitario { get; set; }
        public Nullable<int> pro_stock { get; set; }
        public string pro_url_img { get; set; }
    
        public virtual t_categoria t_categoria { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<t_detalle_venta> t_detalle_venta { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<t_pedido> t_pedido { get; set; }
    }
}
