using System;
using System.Collections.Generic;

namespace RoadTrafficManagement.Criteria;

public partial class RoadwayIssuesTypeCriteria
{
    public uint StartIndex { get; set; }
    public uint Count { get; set; }
    public string? TypeCode { get; set; } = null!;

    public string? TypeName { get; set; } = null!;

    public string? Description { get; set; }

}
