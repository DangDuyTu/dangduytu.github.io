using System;
using System.Collections.Generic;

namespace RoadTrafficManagement.Models;

public partial class User
{
    public int Id { get; set; }

    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public string MobileNumber { get; set; } = null!;

    public string? EmailAddress { get; set; }

    public byte InActive { get; set; }

    public TimeOnly? LastLogin { get; set; }
}
