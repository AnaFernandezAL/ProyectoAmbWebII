using System.ComponentModel.DataAnnotations;

namespace Libreria.Web.ViewModels
{
    public class ViewModelInput
    {
        [Required(ErrorMessage = "Debe ingresar el código del libro.")]
        [Display(Name = "Código del Libro")]
        public int? IdLibro { get; set; }

        [Required(ErrorMessage = "Debe ingresar una cantidad.")]
        [Range(1, int.MaxValue, ErrorMessage = "La cantidad debe ser mayor a cero.")]
        [Display(Name = "Cantidad")]
        public int? Cantidad { get; set; }

        // El precio se muestra como referencia (readonly en interfaz)
        // No se envía al controlador para cálculo de subtotal
        [Display(Name = "Precio")]
        public decimal? Precio { get; set; }
    }
}
