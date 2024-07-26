using System;
using System.Collections.Generic;

namespace RoadTrafficManagement.Criteria;

/// <summary>
/// Cap duong
/// </summary>
public partial class RoadPropertyCriteria
{
    public int StartIndex { get; set; }
    public int Count { get; set; }

    public string? PropertyCode { get; set; } = null!;

    public string? PropertyName { get; set; } = null!;

    public string? Description { get; set; }

}
