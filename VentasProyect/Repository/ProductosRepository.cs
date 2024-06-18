using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using VentasProyect.Models;
using VentasProyect.Models.Productos;

namespace VentasProyect.Repository
{
    public class ProductosRepository
    {
        private readonly VENTAS_DBEntities1 _dbContext;

        public ProductosRepository()
        {
            _dbContext = new VENTAS_DBEntities1();
        }

        public IEnumerable<Models.Productos.Productos> GetAllProductos()
        {
            
                var data = _dbContext.t_producto.Join(
                    _dbContext.t_categoria,
                    producto => producto.cat_id,
                    categoria => categoria.cat_id,
                    (producto, categoria) => new Models.Productos.Productos
                    {
                        pro_id = producto.pro_id,
                        pro_nombre = producto.pro_nombre,
                        pro_descripcion = producto.pro_descripcion,
                        pro_valor_unitario = (int)producto.pro_valor_unitario,
                        pro_stock = (int)producto.pro_stock,
                        pro_url_img = producto.pro_url_img,
                        pro_estado = producto.pro_estado,
                        cat_id = producto.cat_id.ToString(),
                        cat_nombre = categoria.cat_nombre // Aquí obtenemos el nombre de la categoría
                    })
                    .ToList();

                return data ?? new List<Models.Productos.Productos>();
            
        }

        public IEnumerable<Models.Productos.Productos> GetProductos()
        {
            
                var data = _dbContext.t_producto.Join(
                    _dbContext.t_categoria,
                    producto => producto.cat_id,
                    categoria => categoria.cat_id,
                    (producto, categoria) => new Models.Productos.Productos
                    {
                        pro_id = producto.pro_id,
                        pro_nombre = producto.pro_nombre,
                        pro_descripcion = producto.pro_descripcion,
                        pro_valor_unitario = (int)producto.pro_valor_unitario,
                        pro_stock = (int)producto.pro_stock,
                        pro_url_img = producto.pro_url_img,
                        pro_estado = producto.pro_estado,
                        cat_id = producto.cat_id.ToString(),
                        cat_nombre = categoria.cat_nombre // Aquí obtenemos el nombre de la categoría
                    })
                    .Where(p => p.pro_estado != "Inactivo") // Filtrar por estado diferente de "Inactivo"
                    .ToList();

                return data ?? new List<Models.Productos.Productos>();
           
        }

        public void CreateProduct(Productos model)
        {
            int cat_id = Convert.ToInt32(model.cat_id);
            var newData = new t_producto
            {
                pro_id = model.pro_id,
                cat_id = cat_id,
                pro_nombre = model.pro_nombre,
                pro_descripcion = model.pro_descripcion,
                pro_valor_unitario = model.pro_valor_unitario,
                pro_stock = model.pro_stock,
                pro_url_img = model.pro_url_img,
                pro_estado = model.pro_estado
            };

            _dbContext.t_producto.Add(newData);
            _dbContext.SaveChanges();
        }

        public void UpdateProduct(Productos model)
        {
            var existingProduct = _dbContext.t_producto.FirstOrDefault(u => u.pro_id == model.pro_id);

            if (existingProduct != null)
            {
                existingProduct.pro_nombre = model.pro_nombre;
                existingProduct.pro_descripcion = model.pro_descripcion;
                existingProduct.pro_valor_unitario = model.pro_valor_unitario;
                existingProduct.pro_stock = model.pro_stock;
                existingProduct.pro_url_img = model.pro_url_img;
                existingProduct.pro_estado = model.pro_estado;

                _dbContext.SaveChanges();
            }
        }

        public Models.Productos.Productos GetDataById(int id)
        {
            
                var data = _dbContext.t_producto.FirstOrDefault(u => u.pro_id == id);

                if (data != null)
                {
                    return new Models.Productos.Productos
                    {
                        pro_id = data.pro_id,
                        cat_id = data.cat_id.ToString(),
                        pro_nombre = data.pro_nombre,
                        pro_descripcion = data.pro_descripcion,
                        pro_valor_unitario = (int)data.pro_valor_unitario,
                        pro_stock = (int)data.pro_stock,
                        pro_url_img = data.pro_url_img,
                        pro_estado = data.pro_estado
                    };
                }

                return null;
           
        }


        public IEnumerable<Productos> GetDataProductsToSale(IEnumerable<Productos> productosJson)
        {
            var productosList = new List<Productos>();

            foreach (var productoJson in productosJson)
            {
                var productoId = productoJson.pro_id;
                var cantidad = int.Parse(productoJson.pro_cantidad);

               
                var producto = _dbContext.t_producto.Find(productoId);

                if (producto != null)
                {
                    
                    var newProducto = new Productos
                    {
                        pro_id = producto.pro_id,                       
                        pro_nombre = producto.pro_nombre,
                        pro_descripcion = producto.pro_descripcion,
                        pro_valor_unitario = (int)producto.pro_valor_unitario,
                        pro_stock = (int)producto.pro_stock,
                        pro_url_img = producto.pro_url_img,
                        pro_estado = producto.pro_estado,
                        pro_cantidad = Convert.ToString(cantidad) 
                    };

                    productosList.Add(newProducto);
                }
            }

            return productosList.AsEnumerable();
        }

    }
}
