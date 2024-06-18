using System.Collections.Generic;
using System;

namespace VentasProyect.Models.Reportes
{
    public class VentasClientesReporte
    {
        public string Cliente { get; set; }
        public decimal TotalVenta { get; set; }
    }

    public class CategoriaProductoReporte
    {
        public int CategoriaId { get; set; }
        public string CategoriaNombre { get; set; }
    }

    public class SalesByProduct
    {
        public int pro_id { get; set; }
        public string pro_nombre { get; set; }
        public int cantidad_vendida { get; set; }
        public decimal valor_total_ventas { get; set; }
    }

    public class BestSellingProduct
    {
        public int pro_id { get; set; }
        public string pro_nombre { get; set; }
        public int cantidad_vendida { get; set; }
        public decimal valor_total_ventas { get; set; }
    }

    public class LeastSoldProduct
    {
        public int pro_id { get; set; }
        public string pro_nombre { get; set; }
        public int cantidad_vendida { get; set; }
        public decimal valor_total_ventas { get; set; }
    }

    public class ProductOutofStock
    {
        public int pro_id { get; set; }
        public string pro_nombre { get; set; }
        public string pro_descripcion { get; set; }
        public decimal pro_valor_unitario { get; set; }
        public int pro_stock { get; set; }
    }

    public class DateOfSales
    {
        public DateTime ven_fecha { get; set; }
        public int cantidad_ventas { get; set; }
        public decimal total_ventas { get; set; }
    }

    public class StoreCustomers
    {
        public int per_id { get; set; }
        public string per_nombre { get; set; }
        public string per_direccion { get; set; }
        public string per_telefono { get; set; }
        public string per_cuenta_bancaria { get; set; }
        public string per_correo { get; set; }
        public int per_nit { get; set; }
        public string per_tipo { get; set; }
    }

    public class PurchasedByTheCust
    {
        public int PerId { get; set; }
        public string PerNombre { get; set; }
        public int ProId { get; set; }
        public string ProNombre { get; set; }
        public int CantidadComprada { get; set; }
    }

    public class CustomersByProductCategory
    {
        public int PerId { get; set; }
        public string PerNombre { get; set; }
        public int CatId { get; set; }
        public string CatNombre { get; set; }
        public int TotalComprado { get; set; }
    }

    public class t_venta
    {
        public int ven_id { get; set; }
        public DateTime ven_fecha { get; set; }
        public int per_id { get; set; }
        public int usu_id { get; set; }
        public string ven_metodo_pago { get; set; }
        public long ven_total { get; set; }
        public long ven_numero_transaccion { get; set; }

        // Propiedad de navegación a t_persona
        public virtual t_persona Persona { get; set; }

        // Propiedad de navegación a t_usuario
        public virtual t_usuario Usuario { get; set; }
    }

    public class t_persona
    {
        public int per_id { get; set; }
        public string per_nombre { get; set; }

        // Propiedad de navegación a t_venta (uno a muchos)
        public virtual ICollection<t_venta> Ventas { get; set; }
    }

    public class t_usuario
    {
        public int usu_id { get; set; }
        public string usu_nombre { get; set; }

        // Propiedad de navegación a t_venta (uno a muchos)
        public virtual ICollection<t_venta> Ventas { get; set; }
    }

    public class GraficosViewModel
    {
        public List<Dictionary<string, object>> ProductCategory { get; set; }
        public List<Dictionary<string, object>> SalesByProduct { get; set; }
        public List<Dictionary<string, object>> BestSellingProduct { get; set; }
        public List<Dictionary<string, object>> LeastSoldProduct { get; set; }
        public List<Dictionary<string, object>> ProductsOutOfStock { get; set; }
        public List<Dictionary<string, object>> SalesByDate { get; set; }
        public List<Dictionary<string, object>> SalesForClient { get; set; }
        public List<Dictionary<string, object>> StoreCustomers { get; set; }
        public List<Dictionary<string, object>> PurchasedByTheCust { get; set; }
        public List<Dictionary<string, object>> CustomersByProductCategory { get; set; }
        public List<Dictionary<string, object>> SalesList { get; set; }
    }


}