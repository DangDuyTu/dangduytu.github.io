using System;
using System.Collections.Generic;

namespace RoadTrafficManagement.Models;

public partial class RoadSection
{
    public long Id { get; set; }

    public string RoadSessionCode { get; set; } = null!;

    public string? RoadSessionName { get; set; }

    public string? Description { get; set; }

    public string? ChainageFrom { get; set; }

    public string? ChainageTo { get; set; }

    public decimal? KilometerStart { get; set; }

    public decimal? KilometerEnd { get; set; }

    public string? StartGps { get; set; }

    public string? EndGps { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public long RoadId { get; set; }

    public long? SessionTypeId { get; set; }

    public virtual Road Road { get; set; } = null!;

    public virtual RoadSesionType? SessionType { get; set; }
}
