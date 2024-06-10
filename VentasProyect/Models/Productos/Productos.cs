using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VentasProyect.Models.Productos
{
    public class Productos
    {
        public int pro_id { get; set; }

        [Display(Name = "ID Categoria")]
        public string cat_id { get; set; }

        [Display(Name = "Categoria")]
        public string cat_nombre { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [RegularExpression(@"^[^\d]+$", ErrorMessage = "El nombre no debe contener números")]
        [StringLength(70, ErrorMessage = "El nombre excede los 70 caracteres")]
        [Display(Name = "Nombre Producto")]
        public string pro_nombre { get; set; }

        [Display(Name = "Descripcion")]
        public string pro_descripcion { get; set; }

        [Display(Name = "Valor Unitario")]
        [RegularExpression(@"^\d+$", ErrorMessage = "El valor unitario solo debe contener números")]
        public int pro_valor_unitario { get; set; }

        [Display(Name = "Stock")]
        [RegularExpression(@"^\d+$", ErrorMessage = "El stock solo recibe numeros")]
        public int pro_stock { get; set; }

        [Display(Name = "URL Imagen")]
        public string pro_url_img { get; set; }

        [Display(Name = "Estado")]
        public string pro_estado { get; set; }
        public string pro_cantidad { get; set; }
    }
}
