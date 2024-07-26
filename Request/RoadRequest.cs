using System;
using System.Collections.Generic;

namespace RoadTrafficManagement.Request;

public partial class RoadRequest
{
    public uint Id { get; set; }

    public uint? TypeId { get; set; }

    public uint? ParentId { get; set; }

    public string RoadCode { get; set; } = null!;

    public string RoadName { get; set; } = null!;

    public string? ChainageFrom { get; set; }

    public string? ChainageTo { get; set; }

    public string? StartGps { get; set; }
    public decimal? KilometerStart { get; set; }

    public decimal? KilometerEnd { get; set; }

    public string? EndGps { get; set; }

    public string? Description { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

}
