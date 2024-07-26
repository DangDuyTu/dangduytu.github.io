using System;
using System.Collections.Generic;

namespace RoadTrafficManagement.Models;

public partial class Role
{
    public int Id { get; set; }

    public string RoleCode { get; set; } = null!;

    public string RoleName { get; set; } = null!;

    public string? Description { get; set; }

    public byte? InActive { get; set; }

    public virtual ICollection<RoleDetail> RoleDetails { get; set; } = new List<RoleDetail>();
}
