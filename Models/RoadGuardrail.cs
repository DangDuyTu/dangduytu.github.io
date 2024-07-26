using System;
using System.Collections.Generic;

namespace RoadTrafficManagement.Models;

public partial class RoadGuardrail
{
    public long Id { get; set; }

    public string? Description { get; set; }

    public decimal? KilometerStart { get; set; }

    public decimal? KilometerEnd { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public string? Status { get; set; }

    public long RoadId { get; set; }

    public virtual Road Road { get; set; } = null!;
}
