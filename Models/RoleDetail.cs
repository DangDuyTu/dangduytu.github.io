using System;
using System.Collections.Generic;

namespace RoadTrafficManagement.Models;

public partial class RoleDetail
{
    public int Id { get; set; }

    public int RoleId { get; set; }

    public int SubSystemId { get; set; }

    public int CommandId { get; set; }

    public byte? Value { get; set; }

    public virtual RoleCommand Command { get; set; } = null!;

    public virtual Role Role { get; set; } = null!;

    public virtual SubSystem SubSystem { get; set; } = null!;
}
