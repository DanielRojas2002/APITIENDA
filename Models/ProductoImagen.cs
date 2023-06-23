using System;
using System.Collections.Generic;

namespace APITIENDA.Models;

public partial class ProductoImagen
{
    public int IdProductoImagen { get; set; }

    public int IdProducto { get; set; }

    public int Prioridad { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public virtual Producto IdProductoNavigation { get; set; } = null!;
}
