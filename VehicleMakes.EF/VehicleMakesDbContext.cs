using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using VehicleMakes.Model.Model;

namespace VehicleMakes;

public partial class VehicleMakesDbContext : DbContext
{
    public VehicleMakesDbContext()
    {
    }

    public VehicleMakesDbContext(DbContextOptions<VehicleMakesDbContext> options)
        : base(options)
    {
    }
    public DbSet<UserPermission> UserPermissions { get; set; }

    public DbSet<User> Users { get; set; }
    public virtual DbSet<Body> Bodies { get; set; }

    public virtual DbSet<DriveType> DriveTypes { get; set; }

    public virtual DbSet<FuelType> FuelTypes { get; set; }

    public virtual DbSet<Make> Makes { get; set; }

    public virtual DbSet<MakeModel> MakeModels { get; set; }

    public virtual DbSet<SubModel> SubModels { get; set; }

    public virtual DbSet<VehicleDetail> VehicleDetails { get; set; }

    public virtual DbSet<VehicleMasterDetail> VehicleMasterDetails { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-5RPLH84\\SQLEXPRESS;Initial Catalog=VehicleMakesDB;Integrated Security=SSPI;TrustServerCertificate=true ");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("Arabic_CI_AI");

        modelBuilder.Entity<Body>(entity =>
        {
            entity.Property(e => e.BodyId).HasColumnName("BodyID");
            entity.Property(e => e.BodyName).HasMaxLength(50);
        });
        modelBuilder.Entity<UserPermission>().HasKey(e => new { e.UserId, e.PermissionId });
   


        modelBuilder.Entity<DriveType>(entity =>
        {
            entity.Property(e => e.DriveTypeId).HasColumnName("DriveTypeID");
            entity.Property(e => e.DriveTypeName).HasMaxLength(50);
        });

        modelBuilder.Entity<FuelType>(entity =>
        {
            entity.HasKey(e => e.FuelTypeId).HasName("PK_FuleTypes");

            entity.Property(e => e.FuelTypeId).HasColumnName("FuelTypeID");
            entity.Property(e => e.FuelTypeName).HasMaxLength(50);
        });

        modelBuilder.Entity<Make>(entity =>
        {
            entity.HasKey(e => e.MakeId).HasName("PK_Make");

            entity.Property(e => e.MakeId).HasColumnName("MakeID");
            entity.Property(e => e.Make1)
                .HasMaxLength(100)
                .HasColumnName("Make");
        });

        modelBuilder.Entity<MakeModel>(entity =>
        {
            entity.HasKey(e => e.ModelId);

            entity.Property(e => e.ModelId).HasColumnName("ModelID");
            entity.Property(e => e.MakeId).HasColumnName("MakeID");
            entity.Property(e => e.ModelName).HasMaxLength(100);

            entity.HasOne(d => d.Make).WithMany(p => p.MakeModels)
                .HasForeignKey(d => d.MakeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MakeModels_Makes");
        });

        modelBuilder.Entity<SubModel>(entity =>
        {
            entity.Property(e => e.SubModelId).HasColumnName("SubModelID");
            entity.Property(e => e.ModelId).HasColumnName("ModelID");
            entity.Property(e => e.SubModelName).HasMaxLength(100);

            entity.HasOne(d => d.Model).WithMany(p => p.SubModels)
                .HasForeignKey(d => d.ModelId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SubModels_MakeModels");
        });

        modelBuilder.Entity<VehicleDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_CarDetails");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.BodyId).HasColumnName("BodyID");
            entity.Property(e => e.DriveTypeId).HasColumnName("DriveTypeID");
            entity.Property(e => e.Engine).HasMaxLength(100);
            entity.Property(e => e.EngineCc).HasColumnName("Engine_CC");
            entity.Property(e => e.EngineCylinders).HasColumnName("Engine_Cylinders");
            entity.Property(e => e.EngineLiterDisplay)
                .HasColumnType("money")
                .HasColumnName("Engine_Liter_Display");
            entity.Property(e => e.FuelTypeId).HasColumnName("FuelTypeID");
            entity.Property(e => e.MakeId).HasColumnName("MakeID");
            entity.Property(e => e.ModelId).HasColumnName("ModelID");
            entity.Property(e => e.SubModelId).HasColumnName("SubModelID");
            entity.Property(e => e.VehicleDisplayName)
                .HasMaxLength(150)
                .HasColumnName("Vehicle_Display_Name");

            entity.HasOne(d => d.Body).WithMany(p => p.VehicleDetails)
                .HasForeignKey(d => d.BodyId)
                .HasConstraintName("FK_VehicleDetails_Bodies");

            entity.HasOne(d => d.DriveType).WithMany(p => p.VehicleDetails)
                .HasForeignKey(d => d.DriveTypeId)
                .HasConstraintName("FK_VehicleDetails_DriveTypes");

            entity.HasOne(d => d.FuelType).WithMany(p => p.VehicleDetails)
                .HasForeignKey(d => d.FuelTypeId)
                .HasConstraintName("FK_VehicleDetails_FuelTypes");

            entity.HasOne(d => d.SubModel).WithMany(p => p.VehicleDetails)
                .HasForeignKey(d => d.SubModelId)
                .HasConstraintName("FK_VehicleDetails_SubModels");
        });

        modelBuilder.Entity<VehicleMasterDetail>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VehicleMasterDetails");

            entity.Property(e => e.BodyId).HasColumnName("BodyID");
            entity.Property(e => e.BodyName).HasMaxLength(50);
            entity.Property(e => e.DriveTypeId).HasColumnName("DriveTypeID");
            entity.Property(e => e.DriveTypeName).HasMaxLength(50);
            entity.Property(e => e.Engine).HasMaxLength(100);
            entity.Property(e => e.EngineCc).HasColumnName("Engine_CC");
            entity.Property(e => e.EngineCylinders).HasColumnName("Engine_Cylinders");
            entity.Property(e => e.EngineLiterDisplay)
                .HasColumnType("money")
                .HasColumnName("Engine_Liter_Display");
            entity.Property(e => e.FuelTypeId).HasColumnName("FuelTypeID");
            entity.Property(e => e.FuelTypeName).HasMaxLength(50);
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Make).HasMaxLength(100);
            entity.Property(e => e.MakeId).HasColumnName("MakeID");
            entity.Property(e => e.ModelId).HasColumnName("ModelID");
            entity.Property(e => e.ModelName).HasMaxLength(100);
            entity.Property(e => e.SubModelId).HasColumnName("SubModelID");
            entity.Property(e => e.SubModelName).HasMaxLength(100);
            entity.Property(e => e.VehicleDisplayName)
                .HasMaxLength(150)
                .HasColumnName("Vehicle_Display_Name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
