using System;
using System.Collections.Generic;

namespace DL;

public partial class Especialidad
{
    public int IdEspecialidad { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Hospital> Hospitals { get; set; } = new List<Hospital>();
}
