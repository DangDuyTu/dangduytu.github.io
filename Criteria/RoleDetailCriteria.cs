using System;
using System.Collections.Generic;

namespace RoadTrafficManagement.Criteria;

public partial class RoleDetailCriteria
{

    public uint? RoleId { get; set; }

    public uint? SubSystemId { get; set; }

    public uint? CommandId { get; set; }

    public byte? Value { get; set; }

    public string? Description { get; set; }

}