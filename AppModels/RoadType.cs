using System;
using System.Collections.Generic;

namespace RoadTrafficManagement.AppModels;

/// <summary>
/// Loai duong cho tuyen duong thong tin chung
/// </summary>
public partial class RoadType
{
    public uint Id { get; set; }

    /// <summary>
    /// Ma loai
    /// </summary>
    public string TypeCode { get; set; } = null!;

    /// <summary>
    /// Ten loai
    /// </summary>
    public string TypeName { get; set; } = null!;

    /// <summary>
    /// Dien giai
    /// </summary>
    public string? Description { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual ICollection<Road> Roads { get; set; } = new List<Road>();
}
