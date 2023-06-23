using System;
using System.Collections.Generic;

namespace APITIENDA.Models;

public partial class Producto
{
    public int IdProducto { get; set; }

    public int IdCategoria { get; set; }

    public string Nombre { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public double Precio { get; set; }

    public int? Stock { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public virtual ICollection<Almacen> Almacens { get; set; } = new List<Almacen>();

    public virtual ICollection<CarritoDetalle> CarritoDetalles { get; set; } = new List<CarritoDetalle>();

    public virtual Categorium IdCategoriaNavigation { get; set; } = null!;

    public virtual ICollection<PedidoDetalle> PedidoDetalles { get; set; } = new List<PedidoDetalle>();

    public virtual ICollection<ProductoImagen> ProductoImagens { get; set; } = new List<ProductoImagen>();
}
