using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VentasProyect.Models.Persona
{
    public class Persona
    {
        public int per_id { get; set; }

        [Required(ErrorMessage = "La ciudad es obligatoria")]
        [Display(Name = "Ciudad")]
        public string ciu_id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(20, ErrorMessage = "El nombre no es válido")]
        [Display(Name = "Nombre")]
        [RegularExpression(@"^[^\d]+$", ErrorMessage = "El nombre no es válido")]
        public string per_nombre { get; set; }

        [Required(ErrorMessage = "La dirección es obligatoria")]
        [StringLength(100, ErrorMessage = "La dirección no es válida")]
        [Display(Name = "Dirección")]
        [RegularExpression(@"^[A-Za-záéíóúüñÁÉÍÓÚÜÑ]+\s+\d{1,3}\s*#\s*\d{1,3}[A-Za-záéíóúüñÁÉÍÓÚÜÑ]?\s*-\s*\d{1,3}$", ErrorMessage = "La dirección no es válida, Ej: Calle 1 # 2 - 01")]
        public string per_direccion { get; set; }

        [Required(ErrorMessage = "El Teléfono es obligatorio")]
        [RegularExpression(@"^\d{7,10}$", ErrorMessage = "El teléfono debe tener entre 7 y 10 dígitos")]
        [Display(Name = "Teléfono")]
        public string per_telefono { get; set; }

        [Required(ErrorMessage = "La Cuenta Bancaria es obligatoria")]
        [Display(Name = "Cuenta Bancaria")]
        [RegularExpression(@"^\d{11,20}$", ErrorMessage = "La cuenta bancaria no es válida")]
        public string per_cuenta_bancaria { get; set; }

        [Required(ErrorMessage = "El correo electrónico es obligatorio")]
        [EmailAddress(ErrorMessage = "El formato del correo electrónico no es válido")]
        [Display(Name = "Correo Electrónico")]
        public string per_correo { get; set; }

        [Range(1000000, 9999999999, ErrorMessage = "El NIT no es válido")]
        [Display(Name = "NIT")]
        public int per_nit { get; set; }

        [Required(ErrorMessage = "El tipo de persona es obligatorio")]
        [Display(Name = "Tipo Persona")]
        public string per_tipo { get; set; }

        [Required(ErrorMessage = "El tipo de cuenta es obligatorio")]
        [Display(Name = "Tipo Cuenta")]
        public string per_tipo_cuenta { get; set; }

        [Required(ErrorMessage = "El tipo de documento es obligatorio")]
        [Display(Name = "Tipo Documento")]
        public string per_tipo_documento { get; set; }

        [StringLength(50, ErrorMessage = "La vista no puede exceder los 50 caracteres")]
        public string per_vista { get; set; }
    }
}
