using System;
using System.Collections.Generic;

namespace RoadTrafficManagement.Criteria;


public partial class RoadTypeCriteria
{
    public int StartIndex { get; set; }
    public int Count { get; set; }
    public string TypeCode { get; set; } = null!;
    public string TypeName { get; set; } = null!;
    public string? Description { get; set; }

}
