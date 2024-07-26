using System;
using System.Collections.Generic;

namespace RoadTrafficManagement.Models;

public partial class RoadInfrastructure
{
    public long Id { get; set; }

    public string? Chainage { get; set; }

    public string? Description { get; set; }

    public string? Location { get; set; }

    public string? Status { get; set; }

    public DateTime? InstallationDate { get; set; }

    public decimal? Kilometer { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public long PropertyId { get; set; }

    public long RoadId { get; set; }

    public virtual RoadProperty Property { get; set; } = null!;

    public virtual Road Road { get; set; } = null!;
}
