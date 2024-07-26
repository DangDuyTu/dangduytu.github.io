using System;
using System.Collections.Generic;

namespace RoadTrafficManagement.Models;

public partial class SubSystem
{
    public int Id { get; set; }

    public int? ParentId { get; set; }

    public string SubSystemCode { get; set; } = null!;

    public string SubSystemName { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<RoleDetail> RoleDetails { get; set; } = new List<RoleDetail>();
}
