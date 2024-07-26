using System;
using System.Collections.Generic;

namespace RoadTrafficManagement.Request;

public partial class RoadInfrastructureRequest
{
    public uint Id { get; set; }

    public uint PropertyId { get; set; }

    public uint RoadId { get; set; }

    public string? Location { get; set; }

    public string? Chainage { get; set; }

    public decimal? Kilometer { get; set; }

    public DateTime InstallationDate { get; set; }

    public string? Status { get; set; }

    public string? Description { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }
}
