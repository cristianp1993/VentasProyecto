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
    
    public partial class t_pedido
    {
        public int ped_id { get; set; }
        public Nullable<int> pro_id { get; set; }
        public Nullable<int> per_id { get; set; }
        public Nullable<int> ped_cant_solicitada { get; set; }
        public Nullable<int> ped_cant_entregada { get; set; }
        public Nullable<System.DateTime> ped_fecha { get; set; }
    
        public virtual t_persona t_persona { get; set; }
        public virtual t_producto t_producto { get; set; }
    }
}
