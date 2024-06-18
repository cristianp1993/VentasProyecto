using System.ComponentModel.DataAnnotations;

namespace VentasProyect.Models.Ciudad
{
    public class Ciudad
    {
        public int ciu_id { get; set; }
        [Required(ErrorMessage = "El campo es obligatorio.")]
        [Display(Name = "Ciudad")]
        public string ciu_nombre { get; set; }
        [Required(ErrorMessage = "El campo es obligatorio.")]
        [Display(Name = "Departamento")]
        
        public string ciu_depto { get; set; }
    }
}