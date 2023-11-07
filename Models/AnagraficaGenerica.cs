using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebAppTestEmployees.Models;

public partial class AnagraficaGenerica
{
    [Required(ErrorMessage = "La matricola è obbligatoria")]

    [RegularExpression(@"[A-Z]{1}\d{3}$", ErrorMessage = "Il formato è simile a questo \'A123\'")]
    [StringLength(4, ErrorMessage = "La matricola è lunga 4 caratteri")]
    public string Matricola { get; set; } = null!;

    [StringLength(30, MinimumLength = 3, ErrorMessage = "Il nome non può essere più corto di 3 lettere o più lungo di 30")]
    public string Nominativo { get; set; } = null!;

    public string Ruolo { get; set; } = null!;

    public string? Reparto { get; set; }

    [Range (18, 100, ErrorMessage = "L'età non può essere inferiore a 18 anni e superiore ai 100") ]
    public int? Eta { get; set; }

    public string? Indirizzo { get; set; }

    public string? Citta { get; set; }

    public string? Provincia { get; set; }

    public string? Cap { get; set; }

    [Range(10, 15, ErrorMessage = "Il numero di telefono non è corretto")]

    public string? Telefono { get; set; }

    public virtual ICollection<AttivitaDipendente> AttivitaDipendentes { get; set; } = new List<AttivitaDipendente>();
}
