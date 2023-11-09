using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Swashbuckle.AspNetCore.Annotations;

namespace WebAppTestEmployees.Models;

public partial class AttivitaDipendente
{
    public int Id { get; set; }

    [Required(ErrorMessage = "La data è obbligatoria")]

    public DateTime? DataAttivita { get; set; }

    [Required(ErrorMessage = "L'attività è obbligatoria")]

    [StringLength(30, MinimumLength = 3, ErrorMessage = "Il nome non può essere più corto di 3 lettere o più lungo di 30")]

    public string? Attivita { get; set; }

    [Required(ErrorMessage = "Il tempo di attività è obbligatoria")]
    public int? Ore { get; set; }

    [RegularExpression(@"[A-Z]{1}\d{3}$", ErrorMessage = "Il formato è simile a questo \'A123\'")]

    [Required(ErrorMessage = "La matricola è obbligatoria")]

    [StringLength(4, ErrorMessage = "La matricola è lunga 4 caratteri")]

    public string? Matricola { get; set; }

    public virtual AnagraficaGenerica? MatricolaNavigation { get; set; }
}
