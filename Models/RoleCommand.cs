using System;
using System.Collections.Generic;

namespace RoadTrafficManagement.Models;

public partial class RoleCommand
{
    public int Id { get; set; }

    public string CommandCode { get; set; } = null!;

    public string CommandName { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<RoleDetail> RoleDetails { get; set; } = new List<RoleDetail>();
}
