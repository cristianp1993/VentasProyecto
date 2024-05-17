using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VentasProyect.Models;
using VentasProyect.Models.Categoria;

namespace VentasProyect.Repository
{
    public class CategoriaRepository
    {
        private readonly VENTAS_DBEntities1 _dbContext;

        public CategoriaRepository()
        {
            _dbContext = new VENTAS_DBEntities1();
        }
        public IEnumerable<Models.Categoria.Categoria> GetData()
        {
            
            using (VENTAS_DBEntities1 dbContext = new VENTAS_DBEntities1())
            {
                var data = dbContext.t_categoria.Select(xh => new Models.Categoria.Categoria
                {
                    cat_id = xh.cat_id,
                    cat_nombre = xh.cat_nombre  

                }).ToList();

                return data;
            }
        }
        public void Create(Categoria model)
        {
            
            var newData = new t_categoria
            {
                cat_id = model.cat_id,
                cat_nombre = model.cat_nombre

            };

            _dbContext.t_categoria.Add(newData);

            _dbContext.SaveChanges();
        }

        public void Update(Categoria model)
        {
            var existing = _dbContext.t_categoria.FirstOrDefault(u => u.cat_id == model.cat_id);

            if (existing != null)
            {

                existing.cat_id = model.cat_id;
                existing.cat_nombre = model.cat_nombre;              

                _dbContext.SaveChanges();
            }

        }
        public void Delete(int id)
        {
            var toDelete = _dbContext.t_categoria.FirstOrDefault(u => u.cat_id == id);
            if (toDelete != null)
            {
                _dbContext.t_categoria.Remove(toDelete);
                _dbContext.SaveChanges();
            }

        }

        public Models.Categoria.Categoria GetDataById(int id)
        {
            using (VENTAS_DBEntities1 dbContext = new VENTAS_DBEntities1())
            {
                var data = dbContext.t_categoria.FirstOrDefault(u => u.cat_id == id);

                if (data != null)
                {
                    return new Models.Categoria.Categoria
                    {
                        cat_id = data.cat_id,
                        cat_nombre = data.cat_nombre                       

                    };
                }
                else
                {

                    return null;
                }
            }
        }
    }
}