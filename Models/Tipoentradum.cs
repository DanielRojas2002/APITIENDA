using System;
using System.Collections.Generic;

namespace APITIENDA.Models;

public partial class Tipoentradum
{
    public int IdTipoEntrada { get; set; }

    public string Descripcion { get; set; } = null!;

    public virtual ICollection<Almacen> Almacens { get; set; } = new List<Almacen>();
}
