using System;
using System.Collections.Generic;

namespace VehicleMakes;

public partial class DriveType
{
    public int DriveTypeId { get; set; }

    public string DriveTypeName { get; set; } = null!;

    public virtual ICollection<VehicleDetail> VehicleDetails { get; set; } = new List<VehicleDetail>();
}
