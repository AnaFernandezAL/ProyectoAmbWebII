using proyecto.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proyecto.Application.DTOs
{
    public class PujasDTO
    {
        [DisplayName("Identificador")]
        public int PujaId { get; set; }

        [DisplayName("Identificador Subasta")]
        public int SubastaId { get; set; }

        [DisplayName("Identificador Dueño")]
        public int CompradorId { get; set; }

        [DisplayName("Monto")]
        [Required(ErrorMessage = "{0} es requerido")]
        public decimal MontoOfertado { get; set; }

        [DisplayName("Hora Realizada")]
        public DateTime FechaHora { get; set; }

        [DisplayName("Notificado")]
        public bool Notificado { get; set; }

        [DisplayName("Dueño de la puja")]
        public virtual Usuarios Comprador { get; set; } = null!;

        [DisplayName("Subasta Asociada")]
        public virtual Subastas Subasta { get; set; } = null!;
    }
}
