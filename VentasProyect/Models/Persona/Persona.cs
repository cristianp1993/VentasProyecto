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

        [Display(Name = "Ciudad")]
        public string ciu_id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(50, ErrorMessage = "El nombre excede los 50 caracteres")]
        [Display(Name = "Nombre")]
        public string per_nombre { get; set; }

        [Display(Name = "Dirección")]
        public string per_direccion { get; set; }

        [Display(Name = "Teléfono")]
        public string per_telefono { get; set; }

        [Display(Name = "Cuenta Bancaria")]
        public string per_cuenta_bancaria { get; set; }

        [EmailAddress(ErrorMessage = "El formato del correo electrónico no es válido")]
        [Display(Name = "Correo Electrónico")]
        public string per_correo { get; set; }

        [Display(Name = "NIT")]
        public int per_nit { get; set; }

        [Display(Name = "Tipo Persona")]
        public string per_tipo { get; set; }

        [Display(Name = "Tipo Cuenta")]
        public string per_tipo_cuenta { get; set; }

        [Display(Name = "Tipo Documento")]
        public string per_tipo_documento { get; set; }
        public string per_vista { get; set; }
    }
}