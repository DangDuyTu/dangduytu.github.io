using System;
using System.Collections.Generic;

namespace RoadTrafficManagement.AppModels;

/// <summary>
/// Tai san thuoc tinh
/// </summary>
public partial class RoadProperty
{
    public uint Id { get; set; }

    public string PropertyCode { get; set; } = null!;

    public string PropertyName { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual ICollection<RoadInfrastructure> RoadInfrastructures { get; set; } = new List<RoadInfrastructure>();
}
