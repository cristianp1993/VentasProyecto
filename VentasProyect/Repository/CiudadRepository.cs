using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VentasProyect.Models;
using VentasProyect.Models.Ciudad;

namespace VentasProyect.Repository
{
    public class CiudadRepository
    {
        private readonly VENTAS_DBEntities1 _dbContext;

        public CiudadRepository()
        {
            _dbContext = new VENTAS_DBEntities1();
        }
        public IEnumerable<Models.Ciudad.Ciudad> GetData()
        {

            using (VENTAS_DBEntities1 dbContext = new VENTAS_DBEntities1())
            {
                var data = dbContext.t_ciudad.Select(xh => new Models.Ciudad.Ciudad
                {
                    ciu_id = xh.ciu_id,
                    ciu_nombre = xh.ciu_nombre,
                    ciu_depto = xh.ciu_depto,
                  

                }).ToList();

                return data;
            }
        }
        public void CreateCiudad(Ciudad model)
        {

            var newData = new t_ciudad
            {
                ciu_nombre = model.ciu_nombre,
                ciu_depto = model.ciu_depto

            };

            _dbContext.t_ciudad.Add(newData);

            _dbContext.SaveChanges();
        }

        public void UpdateUsuario(Ciudad model)
        {
            var existingUsuario = _dbContext.t_ciudad.FirstOrDefault(u => u.ciu_id == model.ciu_id);

            if (existingUsuario != null)
            {

                existingUsuario.ciu_nombre = model.ciu_nombre;
                existingUsuario.ciu_depto = model.ciu_depto;               


                _dbContext.SaveChanges();
            }

        }
        public void Delete(int id)
        {
            var toDelete = _dbContext.t_ciudad.FirstOrDefault(u => u.ciu_id == id);
            if (toDelete != null)
            {
                _dbContext.t_ciudad.Remove(toDelete);
                _dbContext.SaveChanges();
            }

        }

        public Models.Ciudad.Ciudad GetDataById(int id)
        {
            using (VENTAS_DBEntities1 dbContext = new VENTAS_DBEntities1())
            {
                var data = dbContext.t_ciudad.FirstOrDefault(u => u.ciu_id == id);

                if (data != null)
                {
                    return new Models.Ciudad.Ciudad
                    {
                        ciu_id = data.ciu_id,
                        ciu_nombre = data.ciu_nombre,
                        ciu_depto = data.ciu_depto,
                       
                    };
                }
                else
                {

                    return null;
                }
            }
        }


        public IEnumerable<SelectListItem> GetSelectCiudades()
        {
            var ciudades = _dbContext.t_ciudad.OrderBy(c => c.ciu_nombre)
                              .Select(c => new SelectListItem
                              {
                                  Value = c.ciu_id.ToString(),
                                  Text = c.ciu_nombre
                              })
                              .ToList();

            
            ciudades.Insert(0, new SelectListItem { Value = "0", Text = "Seleccione una ciudad" });

            return ciudades;
        }
    }
}