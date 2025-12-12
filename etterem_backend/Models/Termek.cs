using System;
using System.Collections.Generic;

namespace etterem_backend.Models;

public partial class Termek
{
    public int TermekId { get; set; }

    public string TermekNev { get; set; } = null!;

    public int Ar { get; set; }

    public virtual ICollection<Rendelestetel> Rendelestetels { get; set; } = new List<Rendelestetel>();
}
