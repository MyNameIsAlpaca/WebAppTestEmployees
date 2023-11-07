using System;
using System.Collections.Generic;

namespace WebAppTestEmployees.Models;

public partial class ErrorLog
{
    public DateTime? Data { get; set; }

    public string? Message { get; set; }
}
