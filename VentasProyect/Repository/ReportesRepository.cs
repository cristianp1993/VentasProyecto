using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Dynamic;
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


        public List<Dictionary<string, object>> SalesForClient()
        {
            var salesQuery = from v in _dbContext.t_venta
                             join p in _dbContext.t_persona on v.per_id equals p.per_id
                             group v by p.per_nombre into g
                             orderby g.Sum(x => x.ven_total) descending
                             select new
                             {
                                 Cliente = g.Key,
                                 Total_Venta = g.Sum(x => x.ven_total)
                             };

            var salesResult = salesQuery.ToList();

            var salesDictionaryList = salesResult.Select(item =>
                new Dictionary<string, object>
                {
            { "Cliente", item.Cliente },
            { "Total_Venta", item.Total_Venta }
                }).ToList();

            return salesDictionaryList;
        }

        public List<Dictionary<string, object>> ProductCategory()
        {
            var categoryQuery = from c in _dbContext.t_categoria
                                select new
                                {
                                    cat_id = c.cat_id,
                                    cat_nombre = c.cat_nombre
                                };

            var categoryResult = categoryQuery.ToList();

            var categoryDictionaryList = categoryResult.Select(item =>
                new Dictionary<string, object>
                {
            { "cat_id", item.cat_id },
            { "cat_nombre", item.cat_nombre }
                }).ToList();

            return categoryDictionaryList;
        }

        public List<Dictionary<string, object>> SalesByProduct()
        {
            var salesQuery = from p in _dbContext.t_producto
                             join dv in _dbContext.t_detalle_venta on p.pro_id equals dv.pro_id
                             join v in _dbContext.t_venta on dv.ven_id equals v.ven_id
                             group new { p, dv } by new { p.pro_id, p.pro_nombre } into g
                             orderby g.Sum(x => x.dv.det_cantidad) descending
                             select new SalesByProduct
                             {
                                 pro_id = g.Key.pro_id,
                                 pro_nombre = g.Key.pro_nombre,
                                 cantidad_vendida = (int)g.Sum(x => x.dv.det_cantidad),
                                 valor_total_ventas = (decimal)g.Sum(x => x.dv.det_valor_total)
                             };

            var salesResult = salesQuery.ToList();

            var salesDictionaryList = salesResult.Select(item =>
                new Dictionary<string, object>
                {
            { "pro_id", item.pro_id },
            { "pro_nombre", item.pro_nombre },
            { "cantidad_vendida", item.cantidad_vendida },
            { "valor_total_ventas", item.valor_total_ventas }
                }).ToList();

            return salesDictionaryList;
        }

        public List<Dictionary<string, object>> BestSellingProduct()
        {
            var bestSellingProduct = (from p in _dbContext.t_producto
                                      join dv in _dbContext.t_detalle_venta on p.pro_id equals dv.pro_id
                                      join v in _dbContext.t_venta on dv.ven_id equals v.ven_id
                                      group new { p, dv } by new { p.pro_id, p.pro_nombre } into g
                                      orderby g.Sum(x => x.dv.det_cantidad) descending
                                      select new BestSellingProduct
                                      {
                                          pro_id = g.Key.pro_id,
                                          pro_nombre = g.Key.pro_nombre,
                                          cantidad_vendida = (int)g.Sum(x => x.dv.det_cantidad),
                                          valor_total_ventas = (decimal)g.Sum(x => x.dv.det_valor_total)
                                      }).FirstOrDefault();

            // Convertir el resultado a una lista de diccionarios
            var bestSellingProductList = new List<Dictionary<string, object>>();

            if (bestSellingProduct != null)
            {
                var dict = new Dictionary<string, object>
        {
            { "pro_id", bestSellingProduct.pro_id },
            { "pro_nombre", bestSellingProduct.pro_nombre },
            { "cantidad_vendida", bestSellingProduct.cantidad_vendida },
            { "valor_total_ventas", bestSellingProduct.valor_total_ventas }
        };

                bestSellingProductList.Add(dict);
            }

            return bestSellingProductList;
        }

        public List<Dictionary<string, object>> LeastSoldProduct()
        {
            var leastSoldProduct = (from p in _dbContext.t_producto
                                    join dv in _dbContext.t_detalle_venta on p.pro_id equals dv.pro_id
                                    join v in _dbContext.t_venta on dv.ven_id equals v.ven_id
                                    group new { p, dv } by new { p.pro_id, p.pro_nombre } into g
                                    orderby g.Sum(x => x.dv.det_cantidad) ascending
                                    select new LeastSoldProduct
                                    {
                                        pro_id = g.Key.pro_id,
                                        pro_nombre = g.Key.pro_nombre,
                                        cantidad_vendida = (int)g.Sum(x => x.dv.det_cantidad),
                                        valor_total_ventas = (decimal)g.Sum(x => x.dv.det_valor_total)
                                    }).FirstOrDefault();

            // Convertir el resultado a una lista de diccionarios
            var leastSoldProductList = new List<Dictionary<string, object>>();

            if (leastSoldProduct != null)
            {
                var dict = new Dictionary<string, object>
        {
            { "pro_id", leastSoldProduct.pro_id },
            { "pro_nombre", leastSoldProduct.pro_nombre },
            { "cantidad_vendida", leastSoldProduct.cantidad_vendida },
            { "valor_total_ventas", leastSoldProduct.valor_total_ventas }
        };

                leastSoldProductList.Add(dict);
            }

            return leastSoldProductList;
        }

        public List<Dictionary<string, object>> ProductsOutOfStock()
        {
            var productsQuery = from p in _dbContext.t_producto
                                where p.pro_stock == 0
                                select new ProductOutofStock
                                {
                                    pro_id = p.pro_id,
                                    pro_nombre = p.pro_nombre,
                                    pro_descripcion = p.pro_descripcion,
                                    pro_valor_unitario = (decimal)p.pro_valor_unitario,
                                    pro_stock = (int)p.pro_stock
                                };

            // Convertir el resultado a una lista de diccionarios
            var productsList = productsQuery.ToList();

            var productsDictionaryList = productsList.Select(item =>
                new Dictionary<string, object>
                {
            { "pro_id", item.pro_id },
            { "pro_nombre", item.pro_nombre },
            { "pro_descripcion", item.pro_descripcion },
            { "pro_valor_unitario", item.pro_valor_unitario },
            { "pro_stock", item.pro_stock }
                }).ToList();

            return productsDictionaryList;
        }

        public List<Dictionary<string, object>> SalesByDate()
        {
            var salesQuery = from v in _dbContext.t_venta
                             group v by v.ven_fecha into g
                             orderby g.Key ascending
                             select new DateOfSales
                             {
                                 ven_fecha = (DateTime)g.Key,
                                 cantidad_ventas = g.Count(),
                                 total_ventas = (decimal)g.Sum(x => x.ven_total)
                             };

            // Convertir el resultado a una lista de diccionarios
            var salesList = salesQuery.ToList();

            var salesDictionaryList = salesList.Select(item =>
                new Dictionary<string, object>
                {
            { "ven_fecha", item.ven_fecha },
            { "cantidad_ventas", item.cantidad_ventas },
            { "total_ventas", item.total_ventas }
                }).ToList();

            return salesDictionaryList;
        }

        public List<Dictionary<string, object>> StoreCustomers()
        {
            var customersQuery = from p in _dbContext.t_persona
                                 where p.per_tipo == "Cliente" || p.per_tipo == "Ambos"
                                 select new StoreCustomers
                                 {
                                     per_id = p.per_id,
                                     per_nombre = p.per_nombre,
                                     per_direccion = p.per_direccion,
                                     per_telefono = p.per_telefono,
                                     per_cuenta_bancaria = p.per_cuenta_bancaria,
                                     per_correo = p.per_correo,
                                     per_nit = (int)p.per_nit,
                                     per_tipo = p.per_tipo
                                 };

            // Convertir el resultado a una lista de diccionarios
            var customersList = customersQuery.ToList();

            var customersDictionaryList = customersList.Select(item =>
                new Dictionary<string, object>
                {
            { "per_id", item.per_id },
            { "per_nombre", item.per_nombre },
            { "per_direccion", item.per_direccion },
            { "per_telefono", item.per_telefono },
            { "per_cuenta_bancaria", item.per_cuenta_bancaria },
            { "per_correo", item.per_correo },
            { "per_nit", item.per_nit },
            { "per_tipo", item.per_tipo }
                }).ToList();

            return customersDictionaryList;
        }

        public List<Dictionary<string, object>> PurchasedByTheCust()
        {
            // Consulta para obtener las compras por cliente y producto
            var purchasedQuery = from p in _dbContext.t_persona
                                 join v in _dbContext.t_venta on p.per_id equals v.per_id
                                 join dv in _dbContext.t_detalle_venta on v.ven_id equals dv.ven_id
                                 join pr in _dbContext.t_producto on dv.pro_id equals pr.pro_id
                                 where p.per_tipo == "Cliente" || p.per_tipo == "Ambos"
                                 group new { p, pr, dv } by new { p.per_id, p.per_nombre, pr.pro_id, pr.pro_nombre } into g
                                 select new PurchasedByTheCust
                                 {
                                     PerId = g.Key.per_id,
                                     PerNombre = g.Key.per_nombre,
                                     ProId = g.Key.pro_id,
                                     ProNombre = g.Key.pro_nombre,
                                     CantidadComprada = (int)g.Sum(x => x.dv.det_cantidad)
                                 };

            // Consulta para obtener la máxima cantidad comprada por cliente
            var maxPurchasesQuery = from cte in purchasedQuery
                                    group cte by cte.PerId into g
                                    select new
                                    {
                                        PerId = g.Key,
                                        MaxCantidadComprada = g.Max(x => x.CantidadComprada)
                                    };

            // Consulta final que une purchasedQuery y maxPurchasesQuery
            var resultQuery = from cte in purchasedQuery
                              join maxCompras in maxPurchasesQuery
                              on new { cte.PerId } equals new { maxCompras.PerId }
                              where cte.CantidadComprada == maxCompras.MaxCantidadComprada
                              orderby cte.PerId, cte.ProId
                              select new
                              {
                                  cte.PerId,
                                  cte.PerNombre,
                                  cte.ProId,
                                  cte.ProNombre,
                                  cte.CantidadComprada
                              };

            // Ejecutar la consulta y obtener los resultados
            var result = resultQuery.ToList();

            // Convertir los resultados a una lista de diccionarios
            var resultDictionaryList = result.Select(item =>
                new Dictionary<string, object>
                {
                { "PerId", item.PerId },
                { "PerNombre", item.PerNombre },
                { "ProId", item.ProId },
                { "ProNombre", item.ProNombre },
                { "CantidadComprada", item.CantidadComprada }
                }).ToList();

            return resultDictionaryList;
        }

        public List<Dictionary<string, object>> CustomersByProductCategory()
        {
            var customerCategoryQuery = from p in _dbContext.t_persona
                                        join v in _dbContext.t_venta on p.per_id equals v.per_id
                                        join dv in _dbContext.t_detalle_venta on v.ven_id equals dv.ven_id
                                        join pr in _dbContext.t_producto on dv.pro_id equals pr.pro_id
                                        join c in _dbContext.t_categoria on pr.cat_id equals c.cat_id
                                        where p.per_tipo == "Cliente" || p.per_tipo == "Ambos"
                                        group new { p, c, dv } by new { p.per_id, p.per_nombre, c.cat_id, c.cat_nombre } into g
                                        select new CustomersByProductCategory
                                        {
                                            PerId = g.Key.per_id,
                                            PerNombre = g.Key.per_nombre,
                                            CatId = g.Key.cat_id,
                                            CatNombre = g.Key.cat_nombre,
                                            TotalComprado = (int)g.Sum(x => x.dv.det_cantidad)
                                        };

            var resultQuery = from cte in customerCategoryQuery
                              group cte by new { cte.PerId, cte.PerNombre, cte.CatId, cte.CatNombre } into g
                              select new CustomersByProductCategory
                              {
                                  PerId = g.Key.PerId,
                                  PerNombre = g.Key.PerNombre,
                                  CatId = g.Key.CatId,
                                  CatNombre = g.Key.CatNombre,
                                  TotalComprado = g.Sum(x => x.TotalComprado)
                              };

            var resultList = resultQuery.OrderBy(x => x.CatId).ThenBy(x => x.PerId).ToList();

            var resultDictionaryList = resultList.Select(item =>
                new Dictionary<string, object>
                {
                { "PerId", item.PerId },
                { "PerNombre", item.PerNombre },
                { "CatId", item.CatId },
                { "CatNombre", item.CatNombre },
                { "TotalComprado", item.TotalComprado }
                }).ToList();

            return resultDictionaryList;
        }

        public List<Dictionary<string, object>> SalesList()
        {
            var query = from v in _dbContext.t_venta
                        join p in _dbContext.t_persona on v.per_id equals p.per_id into personaJoin
                        from p in personaJoin.DefaultIfEmpty()
                        join u in _dbContext.t_usuario on v.usu_id equals u.usu_id into usuarioJoin
                        from u in usuarioJoin.DefaultIfEmpty()
                        orderby v.ven_fecha descending, v.ven_id descending
                        select new
                        {
                            v.ven_fecha,
                            v.ven_id,
                            nombre_cliente = p.per_nombre,
                            nombre_usuario = u.usu_nombre,
                            v.ven_metodo_pago,
                            v.ven_total,
                            v.ven_numero_transaccion
                        };

            var salesResult = query.ToList();

            var salesDictionaryList = salesResult.Select(item =>
                new Dictionary<string, object>
                {
            { "Fecha", item.ven_fecha },
            { "ID_Venta", item.ven_id },
            { "Cliente", item.nombre_cliente ?? "N/A" },
            { "Usuario", item.nombre_usuario ?? "N/A" },
            { "Metodo_Pago", item.ven_metodo_pago },
            { "Total_Venta", item.ven_total },
            { "Numero_Transaccion", item.ven_numero_transaccion }
                }).ToList();

            return salesDictionaryList;
        }









    }
}
