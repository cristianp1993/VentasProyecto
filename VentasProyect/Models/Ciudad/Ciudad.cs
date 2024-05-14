using System.ComponentModel.DataAnnotations;

namespace VentasProyect.Models.Ciudad
{
    public class Ciudad
    {
        public int ciu_id { get; set; }
        [Display(Name = "Ciudad")]
        public string ciu_nombre { get; set; }
        [Display(Name = "Departamento")]
        public string ciu_depto { get; set; }
    }
}