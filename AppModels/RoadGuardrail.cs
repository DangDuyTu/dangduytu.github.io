using System;
using System.Collections.Generic;

namespace RoadTrafficManagement.AppModels;

/// <summary>
/// Ho lan
/// </summary>
public partial class RoadGuardrail
{
    public uint Id { get; set; }

    public uint RoadId { get; set; }

    public decimal? KilometerStart { get; set; }

    public decimal? KilometerEnd { get; set; }

    public string? Status { get; set; }

    public string? Description { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual Road Road { get; set; } = null!;
}
