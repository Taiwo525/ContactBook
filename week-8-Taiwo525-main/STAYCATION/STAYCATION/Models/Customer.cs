using System;
using System.Collections.Generic;

namespace STAYCATION.Models;

public partial class Customer
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasssWord { get; set; } = null!;

    public string RepeatPassword { get; set; } = null!;
}
