using System;
using System.Collections.Generic;

namespace RoadTrafficManagement.Models;

public partial class GoverningBody
{
    public long Id { get; set; }

    public string GoverningCode { get; set; } = null!;

    public string GoverningName { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public string? Address { get; set; }

    public string? Description { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }
}
