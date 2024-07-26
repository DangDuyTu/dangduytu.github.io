using System;
using System.Collections.Generic;

namespace RoadTrafficManagement.Criteria;

public partial class RoleCriteria
{

    public string? RoleCode { get; set; } = null!;

    public string? RoleName { get; set; } = null!;

    public string? Description { get; set; }

    public byte? InActive { get; set; }

}
