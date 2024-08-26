using System;
using System.Collections.Generic;

namespace VehicleMakes;

public partial class VehicleDetail
{
    public int Id { get; set; }

    public int? MakeId { get; set; }

    public int? ModelId { get; set; }

    public int? SubModelId { get; set; }

    public int? BodyId { get; set; }

    public string? VehicleDisplayName { get; set; }

    public short? Year { get; set; }

    public int? DriveTypeId { get; set; }

    public string? Engine { get; set; }

    public short? EngineCc { get; set; }

    public byte? EngineCylinders { get; set; }

    public decimal? EngineLiterDisplay { get; set; }

    public int? FuelTypeId { get; set; }

    public byte? NumDoors { get; set; }

    public virtual Body? Body { get; set; }

    public virtual DriveType? DriveType { get; set; }

    public virtual FuelType? FuelType { get; set; }

    public virtual SubModel? SubModel { get; set; }
}
