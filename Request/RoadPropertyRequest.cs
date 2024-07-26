using System;
using System.Collections.Generic;

namespace RoadTrafficManagement.Request;

/// <summary>
/// Cap duong
/// </summary>
public partial class RoadPropertyRequest
{
    public uint Id { get; set; }

    public string PropertyCode { get; set; } = null!;

    public string PropertyName { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }
}
