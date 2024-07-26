using System;
using System.Collections.Generic;

namespace RoadTrafficManagement.Criteria;

/// <summary>
/// Loai dai phan cach
/// </summary>
public partial class RoadGuardrailCriteria
{

    public int StartIndex { get; set; }
    public int Count { get; set; }
    public int RoadId { get; set; }
    public string? Status { get; set; } = null!;
    public decimal KilometerStart { get; set; }
    public decimal KilometerEnd { get; set; }
    public string? Description { get; set; }

}
