using System;
using System.Collections.Generic;

namespace RoadTrafficManagement.Request;


public partial class RoadwayIssueRequest
{
    public uint Id { get; set; }

    public uint TypeId { get; set; }

    public string? IssueCode { get; set; }

    public string IssueName { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime IssueDate { get; set; }
    public DateTime? CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }

}
