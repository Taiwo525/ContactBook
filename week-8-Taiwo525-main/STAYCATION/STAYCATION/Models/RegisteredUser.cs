using System;
using System.Collections.Generic;

namespace STAYCATION.Models;

public partial class RegisteredUser
{
    public Guid Id { get; set; }

    public string? FullName { get; set; }

    public string? Email { get; set; }

    public string? PassWord { get; set; }

    public DateTime? DateTime { get; set; }
}
