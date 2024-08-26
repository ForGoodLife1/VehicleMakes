using System;
using System.Collections.Generic;

namespace VehicleMakes;

public partial class MakeModel
{
    public int ModelId { get; set; }

    public int MakeId { get; set; }

    public string ModelName { get; set; } = null!;

    public virtual Make Make { get; set; } = null!;

    public virtual ICollection<SubModel> SubModels { get; set; } = new List<SubModel>();
}
