using System;
using System.Collections.Generic;

namespace VehicleMakes;

public partial class VehicleMasterDetail
{
    public int Id { get; set; }

    public int? MakeId { get; set; }

    public string Make { get; set; } = null!;

    public int? ModelId { get; set; }

    public string ModelName { get; set; } = null!;

    public int? SubModelId { get; set; }

    public string SubModelName { get; set; } = null!;

    public int? BodyId { get; set; }

    public string BodyName { get; set; } = null!;

    public string? VehicleDisplayName { get; set; }

    public short? Year { get; set; }

    public int? DriveTypeId { get; set; }

    public string DriveTypeName { get; set; } = null!;

    public string? Engine { get; set; }

    public short? EngineCc { get; set; }

    public byte? EngineCylinders { get; set; }

    public decimal? EngineLiterDisplay { get; set; }

    public int? FuelTypeId { get; set; }

    public string FuelTypeName { get; set; } = null!;

    public byte? NumDoors { get; set; }
}
