using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VentasProyect.Models;

namespace VentasProyect.Repository.LoginRepository
{
    public class LoginRepository
    {
        public bool GetSessionStatus(string mail)
        {
            using (VENTAS_DBEntities1 dbContext = new VENTAS_DBEntities1())
            {
                var result = dbContext.t_usuario.Where(xh => xh.usu_correo == mail).Select(xh => xh.usu_sesion_activa).FirstOrDefault();


                if (result != null)
                {
                    return result.HasValue;
                }
                else
                {
                    return false;
                }


            }

        }


        public  bool ValidateLogin(string user, string password)
        {
            //pendiente la encriptacion
            using (VENTAS_DBEntities1 dbContext = new VENTAS_DBEntities1())
            {
                var data = dbContext.t_usuario.Where(xh => xh.usu_correo == user && xh.usu_contrasenia == password).FirstOrDefault();

                bool result = false;

                if (data != null)
                {
                    result = true;
                    return result;
                }
                else
                {
                    return result;
                }
            }
        }
    }
}