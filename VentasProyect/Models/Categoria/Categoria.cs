using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace VentasProyect.Models.Categoria
{
    public class Categoria
    {
        public int cat_id { get; set; }
        [Required(ErrorMessage = "El campo nombre es obligatorio.")]
        [Display(Name = "Nombre")]
        public string cat_nombre { get; set; }
    }
}