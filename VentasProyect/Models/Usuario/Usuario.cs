using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VentasProyect.Models.usuario
{
    public class usuario
    {
        public int usu_id { get; set; }
        public string usu_nombre { get; set; }
        public string usu_contrasenia { get; set; }
        public string usu_rol { get; set; }
        public int usu_sesion_activa { get; set; }
        public string usu_correo { get; set; }
    }
}