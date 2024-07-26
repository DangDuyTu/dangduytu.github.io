using System;
using System.Collections.Generic;

namespace RoadTrafficManagement.AppModels;

/// <summary>
/// Co so ha tang
/// </summary>
public partial class RoadInfrastructure
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

    public virtual RoadProperty Property { get; set; } = null!;

    public virtual Road Road { get; set; } = null!;
}
