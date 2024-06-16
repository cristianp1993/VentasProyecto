using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VentasProyect.Models;
using VentasProyect.Models.Productos;
using VentasProyect.Models.Ventas;

namespace VentasProyect.Repository
{
    public class VentasRepository
    {
        private readonly VENTAS_DBEntities1 _dbContext;

        public VentasRepository()
        {
            _dbContext = new VENTAS_DBEntities1();
        }
        public IEnumerable<Models.Ventas.Ventas> GetAll()
        {
            try
            {
                var query = from v in _dbContext.t_venta
                            join u in _dbContext.t_usuario on v.usu_id equals u.usu_id
                            join p in _dbContext.t_persona on v.per_id equals p.per_id
                            select new Models.Ventas.Ventas
                            {
                                ven_id = v.ven_id,
                                per_nombre = p.per_nombre,
                                per_id = v.per_id,
                                usu_id = v.usu_id,
                                usu_nombre = u.usu_nombre,
                                ven_fecha = v.ven_fecha,
                                ven_metodo_pago = v.ven_metodo_pago,
                                ven_total = (long)v.ven_total,
                                ven_numero_transaccion = (long)v.ven_numero_transaccion
                            };

                return query.ToList();
            }
            catch (Exception ex)
            {

                throw;
            }
           
           
        }

        public void Create(Models.Ventas.Ventas model, List<Productos> products)
        {
            int id = Convert.ToInt32(model.ven_id);
            var newData = new t_venta
            {
                ven_id = model.ven_id,
                per_id = model.per_id,
                usu_id = model.usu_id,
                ven_fecha = model.ven_fecha,
                ven_metodo_pago = model.ven_metodo_pago,
                ven_total = model.ven_total,
                ven_numero_transaccion = model.ven_numero_transaccion
            };

            _dbContext.t_venta.Add(newData);
            _dbContext.SaveChanges();

            var newId = newData.ven_id;

            foreach (var item in products)
            {
                int quantity = Convert.ToInt32(item.pro_cantidad);
                var totalProduct = quantity * item.pro_valor_unitario;

                var newProduct = new t_detalle_venta
                {
                    ven_id = newId,
                    pro_id = item.pro_id,
                    det_cantidad = quantity,
                    det_valor_total = totalProduct

                };

                _dbContext.t_detalle_venta.Add(newProduct);
                _dbContext.SaveChanges();
            }
        }

        public void Update(Models.Ventas.Ventas model)
        {
            var existing = _dbContext.t_venta.FirstOrDefault(u => u.ven_id == model.ven_id);

            if (existing != null)
            {
                existing.per_id = model.per_id;
                existing.usu_id = model.usu_id;
                existing.ven_fecha = model.ven_fecha;
                existing.ven_metodo_pago = model.ven_metodo_pago;
                existing.ven_total = model.ven_total;               
                existing.ven_numero_transaccion = model.ven_numero_transaccion;               

                _dbContext.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var toDelete = _dbContext.t_venta.FirstOrDefault(u => u.ven_id == id);
            if (toDelete != null)
            {
                _dbContext.t_venta.Remove(toDelete);
                _dbContext.SaveChanges();
            }
        }

        public Models.Ventas.VentaConDetalle GetDataById(int id)
        {
            VentaConDetalle nuevaVenta = new VentaConDetalle();

            // Consulta para obtener la cabecera de la venta
            var venta = _dbContext.t_venta.FirstOrDefault(u => u.ven_id == id);
            if (venta != null)
            {
                nuevaVenta.head = new Ventas
                {
                    per_id = venta.per_id,
                    usu_id = venta.usu_id,
                    ven_fecha = venta.ven_fecha,
                    ven_metodo_pago = venta.ven_metodo_pago,
                    ven_total = (int)venta.ven_total,
                    ven_numero_transaccion = (int)venta.ven_numero_transaccion
                };
            }

            // Consulta para obtener el detalle de la venta
            nuevaVenta.detail = _dbContext.t_detalle_venta
            .Where(u => u.ven_id == id)
            .Join(_dbContext.t_producto, 
                  detalle => detalle.pro_id,
                  producto => producto.pro_id, 
                  (detalle, producto) => new DetalleVenta
                  {
                      ven_id = (int)detalle.ven_id,
                      pro_id = (int)detalle.pro_id,
                      det_cantidad = (int)detalle.det_cantidad,
                      det_valor_total = (long)detalle.det_valor_total,
                      pro_nombre = producto.pro_nombre 
                  })
            .ToList();

            return nuevaVenta;
        }

    }
}