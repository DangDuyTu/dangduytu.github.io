using System;
using System.Collections.Generic;

namespace RoadTrafficManagement.AppModels;

/// <summary>
/// Tuyen duong quan ly
/// </summary>
public partial class RoadSection
{
    public uint Id { get; set; }

    public uint RoadId { get; set; }

    public uint? SessionTypeId { get; set; }

    /// <summary>
    /// Ma tuyen
    /// </summary>
    public string RoadSessionCode { get; set; } = null!;

    /// <summary>
    /// Ten tuyen
    /// </summary>
    public string RoadSessionName { get; set; } = null!;

    /// <summary>
    /// Ly trinh bat dau
    /// </summary>
    public string ChainageFrom { get; set; } = null!;

    /// <summary>
    /// Ly trinh ket thuc
    /// </summary>
    public string ChainageTo { get; set; } = null!;

    public decimal? KilometerStart { get; set; }

    public decimal? KilometerEnd { get; set; }

    /// <summary>
    /// GPS bat dau
    /// </summary>
    public string? StartGps { get; set; }

    public string? EndGps { get; set; }

    public string? Description { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual Road Road { get; set; } = null!;

    public virtual RoadSesionType? SessionType { get; set; }
}
