using System;
using System.Collections.Generic;
using System.Linq;
using VentasProyect.Models;

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
                    usu_rol = user.usu_rol,
                    usu_correo = user.usu_correo

                }).ToList();

                return data;
            }
        }
    }
}