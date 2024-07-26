using System;
using System.Collections.Generic;

namespace RoadTrafficManagement.Request;

public partial class RoadTypeRequest
{
    public uint Id { get; set; }

    public string TypeCode { get; set; } = null!;

    public string TypeName { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

}
