using System;
using System.Collections.Generic;
using System.Linq;
using VentasProyect.Models;
using VentasProyect.Models.usuario;

namespace VentasProyect.Repository
{
    public class UsuarioRepository 
    {
        
        public IEnumerable<Models.usuario.usuario> GetUsuarios()
        {

            using (VENTAS_DBEntities1 dbContext = new VENTAS_DBEntities1())
            {
                var data = dbContext.t_usuario.Select(user => new Models.usuario.usuario
                {
                    usu_id = user.usu_id,
                    usu_nombre = user.usu_nombre,
                    usu_contrasenia = user.usu_contrasenia,
                    usu_rol = user.usu_rol,
                    usu_correo = user.usu_correo

                }).ToList();

                return data;
            }
        }

        internal Models.usuario.usuario GetUsuarioById(int id)
        {
            using (VENTAS_DBEntities1 dbContext = new VENTAS_DBEntities1())
            {
                var user = dbContext.t_usuario.FirstOrDefault(u => u.usu_id == id);

                if (user != null)
                {
                    return new Models.usuario.usuario
                    {
                        usu_id = user.usu_id,
                        usu_nombre = user.usu_nombre,
                        usu_contrasenia = user.usu_contrasenia,
                        usu_rol = user.usu_rol,
                        usu_correo = user.usu_correo
                    };
                }
                else
                {
                    
                    return null;
                }
            }
        }

        private readonly VENTAS_DBEntities1 _dbContext;

        public UsuarioRepository()
        {
            _dbContext = new VENTAS_DBEntities1();
        }

        

        public void UpdateUsuario(usuario usuario)
        {
            var existingUsuario = _dbContext.t_usuario.FirstOrDefault(u => u.usu_id == usuario.usu_id);

            if (existingUsuario != null)
            {
                
                existingUsuario.usu_nombre = usuario.usu_nombre;
                existingUsuario.usu_contrasenia = usuario.usu_contrasenia;
                existingUsuario.usu_rol = usuario.usu_rol;
                existingUsuario.usu_correo = usuario.usu_correo;

                
                _dbContext.SaveChanges();
            }
            
        }

        internal void CreateUsuario(usuario usuario)
        {
            
            var newUser = new t_usuario
            {
                usu_nombre = usuario.usu_nombre,
                usu_contrasenia = usuario.usu_contrasenia,
                usu_rol = usuario.usu_rol,
                usu_correo = usuario.usu_correo
                
            };

            
            _dbContext.t_usuario.Add(newUser);

            
            _dbContext.SaveChanges();
        }

        internal void DeleteUsuario(int id)
        {
            var usuarioToDelete = _dbContext.t_usuario.FirstOrDefault(u => u.usu_id == id);
            if (usuarioToDelete != null)
            {
                _dbContext.t_usuario.Remove(usuarioToDelete);
                _dbContext.SaveChanges();
            }
            
        }
    }
}