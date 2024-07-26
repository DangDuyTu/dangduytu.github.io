using System;
using System.Collections.Generic;

namespace RoadTrafficManagement.Criteria;


public partial class RoadwayIssueCriteria
{
    public uint StartIndex { get; set; }
    public uint Count { get; set; }
    public uint? TypeId { get; set; }

    public string? IssueCode { get; set; }

    public string? IssueName { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime? IssueDate { get; set; }

}
