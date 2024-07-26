using System;
using System.Collections.Generic;

namespace RoadTrafficManagement.Models;

public partial class Road
{
    public long Id { get; set; }

    public string RoadCode { get; set; } = null!;

    public string? RoadName { get; set; }

    public string? Description { get; set; }

    public string? ChainageFrom { get; set; }

    public string? ChainageTo { get; set; }

    public decimal? KilometerStart { get; set; }

    public decimal? KilometerEnd { get; set; }

    public string? StartGps { get; set; }

    public string? EndGps { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public long? ParentId { get; set; }

    public long? TypeId { get; set; }

    public virtual ICollection<Road> InverseParent { get; set; } = new List<Road>();

    public virtual Road? Parent { get; set; }

    public virtual ICollection<RoadGuardrail> RoadGuardrails { get; set; } = new List<RoadGuardrail>();

    public virtual ICollection<RoadInfrastructure> RoadInfrastructures { get; set; } = new List<RoadInfrastructure>();

    public virtual ICollection<RoadSection> RoadSections { get; set; } = new List<RoadSection>();

    public virtual RoadType? Type { get; set; }
}
