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

    [Required]

    [StringLength(30, MinimumLength = 3, ErrorMessage = "Il nome non può essere più corto di 3 lettere o più lungo di 30")]
    public string Nominativo { get; set; } = null!;

    [Required]

    [StringLength(30, MinimumLength = 3, ErrorMessage = "Il ruolo non può essere più corto di 3 lettere o più lungo di 30")]

    public string Ruolo { get; set; } = null!;

    [StringLength(30, MinimumLength = 3, ErrorMessage = "Il Reparto non può essere più corto di 3 lettere o più lungo di 30")]

    public string? Reparto { get; set; }

    [Range (18, 100, ErrorMessage = "L'età non può essere inferiore a 18 anni e superiore ai 100") ]
    public int? Eta { get; set; }

    [StringLength(50, MinimumLength = 3, ErrorMessage = "L'indirizzo non può essere più corto di 3 lettere o più lungo di 50")]

    public string? Indirizzo { get; set; }

    [StringLength(20, MinimumLength = 3, ErrorMessage = "La città non può essere più corta di 3 lettere o più lungo di 20")]

    public string? Citta { get; set; }

    [StringLength(4, MinimumLength = 2, ErrorMessage = "La provincia non può essere più corta di 2 lettere o più lungo di 4")]

    public string? Provincia { get; set; }

    [RegularExpression(@"\d{5}", ErrorMessage = "Il cap è composto da numeri 5")]

    public string? Cap { get; set; }

    [RegularExpression(@"\d+", ErrorMessage = "Il numero di telefono è composto solo da numeri")]

    [StringLength(13, MinimumLength = 10, ErrorMessage = "Il numero di telefono non è corretto")]
    public string? Telefono { get; set; }

    public virtual ICollection<AttivitaDipendente> AttivitaDipendentes { get; set; } = new List<AttivitaDipendente>();
}
