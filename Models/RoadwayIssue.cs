using System;
using System.Collections.Generic;

namespace RoadTrafficManagement.Models;

public partial class RoadwayIssue
{
    public long Id { get; set; }

    public string IssueCode { get; set; } = null!;

    public string? IssueName { get; set; }

    public string? Description { get; set; }

    public DateTime? IssueDate { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public long TypeId { get; set; }

    public virtual RoadwayIssuesType Type { get; set; } = null!;
}
