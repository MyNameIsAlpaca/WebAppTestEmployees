using System;
using System.Collections.Generic;

namespace WebAppTestEmployees.Models;

public partial class AttivitaDipendente
{
    public int Id { get; set; }

    public DateTime? DataAttivita { get; set; }

    public string? Attivita { get; set; }

    public int? Ore { get; set; }

    public string? Matricola { get; set; }

    public virtual AnagraficaGenerica? MatricolaNavigation { get; set; }
}
