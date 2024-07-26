﻿using System;
using System.Collections.Generic;

namespace RoadTrafficManagement.Models;

public partial class RoadwayIssuesType
{
    public long Id { get; set; }

    public string TypeCode { get; set; } = null!;

    public string? TypeName { get; set; }

    public string? Description { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual ICollection<RoadwayIssue> RoadwayIssues { get; set; } = new List<RoadwayIssue>();
}
