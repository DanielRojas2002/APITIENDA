using System;
using System.Collections.Generic;

namespace APITIENDA.Models;

public partial class ProductoM
{
    public int IdProducto { get; set; }

    public int IdCategoria { get; set; }

    public string Nombre { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public double Precio { get; set; }

    public int? Stock { get; set; }

    public DateTime? FechaRegistro { get; set; }


}
