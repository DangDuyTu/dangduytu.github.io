using System;
using System.Collections.Generic;

namespace RoadTrafficManagement.AppModels;


public partial class RoadSectionRequest
{
    public uint Id { get; set; }

    public uint RoadId { get; set; }

    public uint? SessionTypeId { get; set; }

    public string RoadSessionCode { get; set; } = null!;

    public string RoadSessionName { get; set; } = null!;

    public string ChainageFrom { get; set; } = null!;

    public string ChainageTo { get; set; } = null!;

    public string? StartGps { get; set; }

    public string? EndGps { get; set; }

    public string? Description { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

}
