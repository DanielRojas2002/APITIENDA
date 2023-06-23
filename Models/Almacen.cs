using System;
using System.Collections.Generic;

namespace APITIENDA.Models;

public partial class Almacen
{
    public int IdAlmacen { get; set; }

    public int IdProducto { get; set; }

    public int IdTipoEntrada { get; set; }

    public DateTime FechaRegistro { get; set; }

    public int Cantidad { get; set; }

    public virtual Producto IdProductoNavigation { get; set; } = null!;

    public virtual Tipoentradum IdTipoEntradaNavigation { get; set; } = null!;
}
