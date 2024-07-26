using System;
using System.Collections.Generic;

namespace RoadTrafficManagement.Models;

public partial class CameraRecord
{
    public int Id { get; set; }

    public string CameraId { get; set; } = null!;

    public string CameraCode { get; set; } = null!;

    public string CameraName { get; set; } = null!;

    public string? Description { get; set; }

    public string Location { get; set; } = null!;

    public string ChainageFrom { get; set; } = null!;

    public string? ServerAddress { get; set; }

    public string? AccessToken { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }
}
