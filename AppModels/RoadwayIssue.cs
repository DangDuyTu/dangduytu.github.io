using System;
using System.Collections.Generic;

namespace RoadTrafficManagement.AppModels;

/// <summary>
/// Su co tuyen duong quan ly
/// </summary>
public partial class RoadwayIssue
{
    public uint Id { get; set; }

    public uint TypeId { get; set; }

    public string IssueCode { get; set; } = null!;

    public string IssueName { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime IssueDate { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime ModifiedDate { get; set; }

    public virtual RoadwayIssuesType Type { get; set; } = null!;
}
