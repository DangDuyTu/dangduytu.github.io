using System;
using System.Collections.Generic;

namespace RoadTrafficManagement.Models;

public partial class RoadSesionType
{
    public long Id { get; set; }

    public string TypeCode { get; set; } = null!;

    public string? TypeName { get; set; }

    public string? Description { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public virtual ICollection<RoadSection> RoadSections { get; set; } = new List<RoadSection>();
}
