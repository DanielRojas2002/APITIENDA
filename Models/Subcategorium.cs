using System;
using System.Collections.Generic;

namespace APITIENDA.Models;

public partial class Subcategorium
{
    public int IdSubCategoria { get; set; }

    public int IdCategoria { get; set; }

    public string Descripcion { get; set; } = null!;

    public virtual Categorium IdCategoriaNavigation { get; set; } = null!;
}
