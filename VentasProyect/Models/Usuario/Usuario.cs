using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VentasProyect.Models.usuario
{
    [Table("t_usuario")]
    public class usuario
    {
        public int usu_id { get; set; }

        [Display(Name = "Nombre completo")]
        public string usu_nombre { get; set; }

        [Display(Name = "Contraseña")]
        public string usu_contrasenia { get; set; }

        [Display(Name = "Rol")]
        public string usu_rol { get; set; }
        public int usu_sesion_activa { get; set; }

        [Display(Name = "Correo")]
        public string usu_correo { get; set; }
    }
}