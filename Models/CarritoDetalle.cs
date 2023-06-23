using System;
using System.Collections.Generic;

namespace APITIENDA.Models;

public partial class CarritoDetalle
{
    public int IdCarritoDetalle { get; set; }

    public int IdCarrito { get; set; }

    public int IdProducto { get; set; }

    public int Stock { get; set; }

    public virtual Carrito IdCarritoNavigation { get; set; } = null!;

    public virtual Producto IdProductoNavigation { get; set; } = null!;
}
