using System;
using System.Collections.Generic;

namespace RoadTrafficManagement.Request;

public partial class RoleDetailRequest
{
    public uint Id { get; set; }

    public uint RoleId { get; set; }

    public uint SubSystemId { get; set; }

    public uint CommandId { get; set; }

    public byte? Value { get; set; }

    public string? Description { get; set; }

}
