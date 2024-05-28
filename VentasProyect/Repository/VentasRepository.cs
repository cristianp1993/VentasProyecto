using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VentasProyect.Models;

namespace VentasProyect.Repository
{
    public class VentasRepository
    {
        
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
                                ven_total = v.ven_total,
                                ven_numero_transaccion = v.ven_numero_transaccion
                            };

                return query.ToList();
            }
        }
    }
}