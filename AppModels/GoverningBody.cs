using System;
using System.Collections.Generic;

namespace RoadTrafficManagement.AppModels;

/// <summary>
/// Đơn vị chủ quản
/// </summary>
public partial class GoverningBody
{
    public uint Id { get; set; }

    /// <summary>
    /// Ma don vi
    /// </summary>
    public string GoverningCode { get; set; } = null!;

    /// <summary>
    /// Ten don vi
    /// </summary>
    public string GoverningName { get; set; } = null!;

    /// <summary>
    /// Dien thoai
    /// </summary>
    public string? PhoneNumber { get; set; }

    /// <summary>
    /// Dia chi
    /// </summary>
    public string? Address { get; set; }

    /// <summary>
    /// Dien giai
    /// </summary>
    public string? Description { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }
}
