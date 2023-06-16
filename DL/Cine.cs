using System;
using System.Collections.Generic;

namespace DL;

public partial class Cine
{
    public int IdCine { get; set; }

    public string? Complejo { get; set; }

    public string? Direccion { get; set; }
    public string? NombreZona { get; set; }

    public decimal? Venta { get; set; }

    public int? IdZona { get; set; }

    public decimal? Latitud { get; set; }

    public decimal? Longitud { get; set; }

    public virtual Zona? IdZonaNavigation { get; set; }
}
