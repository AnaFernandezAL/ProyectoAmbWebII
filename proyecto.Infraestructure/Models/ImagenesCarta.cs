using System;
using System.Collections.Generic;

namespace proyecto.Infraestructure.Models;

public partial class ImagenesCarta
{
    public int ImagenId { get; set; }

    public int CartaId { get; set; }

    public string Urlimagen { get; set; } = null!;

    public bool EsPrincipal { get; set; }

    public virtual Cartas Carta { get; set; } = null!;
}
