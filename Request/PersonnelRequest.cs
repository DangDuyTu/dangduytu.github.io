﻿using System;
using System.Collections.Generic;

namespace RoadTrafficManagement.Request;

public partial class PersonnelRequest
{
    public uint Id { get; set; }

    public string PersonelCode { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public string? MobilePhoneNumber { get; set; }

    public string? Department { get; set; }

    public string? EmploymentType { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime ModifiedDate { get; set; }
}
