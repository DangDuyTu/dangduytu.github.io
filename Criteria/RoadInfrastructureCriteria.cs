using System;
using System.Collections.Generic;

namespace RoadTrafficManagement.Criteria;

public partial class RoadInfrastructureCriteria
{
    public uint StartIndex { get; set; } = 0;
    public uint Count { get; set; } = 30;
    public uint Id { get; set; }

    public uint? PropertyId { get; set; }

    public uint? RoadId { get; set; }

    public string? Location { get; set; }

    public string? Chainage { get; set; }

    public decimal? Kilometer { get; set; }

    public DateTime? InstallationDate { get; set; }

    public string? Status { get; set; }

    public string? Description { get; set; }

}
