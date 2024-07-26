using System;
using System.Collections.Generic;

namespace RoadTrafficManagement.Models;

public partial class Personnel
{
    public long Id { get; set; }

    public string PersonelCode { get; set; } = null!;

    public string? FullName { get; set; }

    public string? Department { get; set; }

    public string? EmploymentType { get; set; }

    public string? MobilePhoneNumber { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }
}
