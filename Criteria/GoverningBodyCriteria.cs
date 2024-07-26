using System;
using System.Collections.Generic;

namespace RoadTrafficManagement.Criteria;


public partial class GoverningBodyCriteria
{
    public uint StartIndex { get; set; }
    public uint Count { get; set; }
    public string? GoverningCode { get; set; } = null!;

    public string? GoverningName { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public string? Address { get; set; }

    public string? Description { get; set; }

}
