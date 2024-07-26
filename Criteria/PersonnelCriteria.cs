using System;
using System.Collections.Generic;

namespace RoadTrafficManagement.Criteria;

public partial class PersonnelCriteria
{
    public int StartIndex { get; set; } = 0;
    public int Count { get; set; }
    public string? PersonelCode { get; set; } = null!;
    public string? FullName { get; set; } = null!;
    public string? MobilePhoneNumber { get; set; }
    public string? Department { get; set; }
    public string? EmploymentType { get; set; }

}
