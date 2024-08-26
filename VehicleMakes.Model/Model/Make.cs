using System;
using System.Collections.Generic;

namespace VehicleMakes;

public partial class Make
{
    public int MakeId { get; set; }

    public string Make1 { get; set; } = null!;

    public virtual ICollection<MakeModel> MakeModels { get; set; } = new List<MakeModel>();
}
