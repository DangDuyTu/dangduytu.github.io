using System;
using System.Collections.Generic;

namespace RoadTrafficManagement.Models;

public partial class RoadProperty
{
    public long Id { get; set; }

    public string PropertyCode { get; set; } = null!;

    public string? PropertyName { get; set; }

    public string? Description { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual ICollection<RoadInfrastructure> RoadInfrastructures { get; set; } = new List<RoadInfrastructure>();
}
