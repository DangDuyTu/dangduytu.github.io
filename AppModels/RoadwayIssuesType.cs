using System;
using System.Collections.Generic;

namespace RoadTrafficManagement.AppModels;

public partial class RoadwayIssuesType
{
    public uint Id { get; set; }

    public string TypeCode { get; set; } = null!;

    public string TypeName { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual ICollection<RoadwayIssue> RoadwayIssues { get; set; } = new List<RoadwayIssue>();
}
