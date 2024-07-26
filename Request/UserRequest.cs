using System;
using System.Collections.Generic;

namespace RoadTrafficManagement.Request;

public partial class UserRequest
{
    public uint Id { get; set; }

    public string UserName { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public string MobileNumber { get; set; } = null!;

    public string? EmailAddress { get; set; }

    public sbyte InActive { get; set; }

    public DateTime? LastLogin { get; set; }

}
