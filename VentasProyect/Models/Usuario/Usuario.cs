using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VentasProyect.Models.usuario
{
    [Table("t_usuario")]
    public class usuario
    {
        public int usu_id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(20, ErrorMessage = "El nombre no es válido")]
        [Display(Name = "Nombre completo")]
        public string usu_nombre { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [Display(Name = "Contraseña")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
            ErrorMessage = "Debe tener al menos 8 caracteres, incluyendo una mayúscula, una minúscula, un número y un carácter especial.")]
        public string usu_contrasenia { get; set; }

        [Required(ErrorMessage = "El Rol es obligatorio")]
        [Display(Name = "Rol")]
        public string usu_rol { get; set; }
        public int usu_sesion_activa { get; set; }
        
        [Required(ErrorMessage = "El correo electrónico es obligatorio")]
        [EmailAddress(ErrorMessage = "El formato del correo electrónico no es válido")]
        [Display(Name = "Correo")]
        public string usu_correo { get; set; }
    }
}