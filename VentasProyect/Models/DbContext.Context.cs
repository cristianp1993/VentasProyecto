﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class VENTAS_DBEntities1 : DbContext
    {
        public VENTAS_DBEntities1()
            : base("name=VENTAS_DBEntities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<t_categoria> t_categoria { get; set; }
        public virtual DbSet<t_ciudad> t_ciudad { get; set; }
        public virtual DbSet<t_detalle_venta> t_detalle_venta { get; set; }
        public virtual DbSet<t_notificaciones> t_notificaciones { get; set; }
        public virtual DbSet<t_pedido> t_pedido { get; set; }
        public virtual DbSet<t_persona> t_persona { get; set; }
        public virtual DbSet<t_producto> t_producto { get; set; }
        public virtual DbSet<t_usuario> t_usuario { get; set; }
        public virtual DbSet<t_venta> t_venta { get; set; }
    }
}
