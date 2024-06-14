using System;
using System.Collections.Generic;
using System.Linq;
using VentasProyect.Models;

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
            using (VENTAS_DBEntities1 dbContext = new VENTAS_DBEntities1())
            {
                var query = from v in dbContext.t_venta
                            join u in dbContext.t_usuario on v.usu_id equals u.usu_id
                            join p in dbContext.t_persona on v.per_id equals p.per_id
                            select new Models.Ventas.Ventas
                            {
                                ven_id = v.ven_id,
                                per_nombre = p.per_nombre,
                                per_id = v.per_id,
                                usu_id = v.usu_id,
                                usu_nombre = u.usu_nombre,
                                ven_fecha = v.ven_fecha,
                                ven_metodo_pago = v.ven_metodo_pago,
                                ven_total = (int?)v.ven_total, // Conversión explícita
                                ven_numero_transaccion = (int?)v.ven_numero_transaccion // Conversión explícita
                            };

                return query.ToList();
            }
        }

        public void Create(Models.Ventas.Ventas model)
        {
            var newData = new t_venta
            {
                ven_id = model.ven_id,
                per_id = model.per_id,
                usu_id = model.usu_id,
                ven_fecha = model.ven_fecha,
                ven_metodo_pago = model.ven_metodo_pago,
                ven_total = model.ven_total.HasValue ? (long)model.ven_total : (long?)null, // Conversión explícita
                ven_numero_transaccion = model.ven_numero_transaccion.HasValue ? (long)model.ven_numero_transaccion : (long?)null // Conversión explícita
            };

            _dbContext.t_venta.Add(newData);
            _dbContext.SaveChanges();
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
                existing.ven_total = model.ven_total.HasValue ? (long)model.ven_total : (long?)null; // Conversión explícita
                existing.ven_numero_transaccion = model.ven_numero_transaccion.HasValue ? (long)model.ven_numero_transaccion : (long?)null; // Conversión explícita

                _dbContext.SaveChanges();
            }
        }

        public void Delete(long id)
        {
            var toDelete = _dbContext.t_venta.FirstOrDefault(u => u.ven_id == id);
            if (toDelete != null)
            {
                _dbContext.t_venta.Remove(toDelete);
                _dbContext.SaveChanges();
            }
        }

        public Models.Ventas.Ventas GetDataById(long id)
        {
            var data = _dbContext.t_venta.FirstOrDefault(u => u.ven_id == id);

            if (data != null)
            {
                return new Models.Ventas.Ventas
                {
                    ven_id = data.ven_id,
                    per_id = data.per_id,
                    usu_id = data.usu_id,
                    ven_fecha = data.ven_fecha,
                    ven_metodo_pago = data.ven_metodo_pago,
                    ven_total = (int?)data.ven_total, // Conversión explícita
                    ven_numero_transaccion = (int?)data.ven_numero_transaccion // Conversión explícita
                };
            }
            else
            {
                return null;
            }
        }
    }
}
