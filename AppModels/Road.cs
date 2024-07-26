using System;
using System.Collections.Generic;

namespace RoadTrafficManagement.AppModels;

/// <summary>
/// Tuyen duong thong tin chung
/// </summary>
public partial class Road
{
    public uint Id { get; set; }

    public uint? TypeId { get; set; }

    public uint? ParentId { get; set; }

    /// <summary>
    /// Ma duong
    /// </summary>
    public string RoadCode { get; set; } = null!;

    /// <summary>
    /// Ten duong
    /// </summary>
    public string RoadName { get; set; } = null!;

    /// <summary>
    /// Diem bat dau (km)
    /// </summary>
    public string? ChainageFrom { get; set; }

    /// <summary>
    /// Diem ket thuc (km)
    /// </summary>
    public string? ChainageTo { get; set; }

    public decimal? KilometerStart { get; set; }

    public decimal? KilometerEnd { get; set; }

    /// <summary>
    /// Toa do GPS bat dau
    /// </summary>
    public string? StartGps { get; set; }

    /// <summary>
    /// Toa do GPS ket thuc
    /// </summary>
    public string? EndGps { get; set; }

    public string? Description { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual ICollection<Road> InverseParent { get; set; } = new List<Road>();

    public virtual Road? Parent { get; set; }

    public virtual ICollection<RoadGuardrail> RoadGuardrails { get; set; } = new List<RoadGuardrail>();

    public virtual ICollection<RoadInfrastructure> RoadInfrastructures { get; set; } = new List<RoadInfrastructure>();

    public virtual ICollection<RoadSection> RoadSections { get; set; } = new List<RoadSection>();

    public virtual RoadType? Type { get; set; }
}
