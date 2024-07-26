using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace RoadTrafficManagement.Models;

public partial class MyDbContext : DbContext
{
    public MyDbContext()
    {
    }

    public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CameraRecord> CameraRecords { get; set; }

    public virtual DbSet<GoverningBody> GoverningBodies { get; set; }

    public virtual DbSet<Personnel> Personnel { get; set; }

    public virtual DbSet<Road> Roads { get; set; }

    public virtual DbSet<RoadInfrastructure> RoadInfrastructures { get; set; }

    public virtual DbSet<RoadSection> RoadSections { get; set; }

    public virtual DbSet<RoadSesionInfrastructure> RoadSesionInfrastructures { get; set; }

    public virtual DbSet<RoadSesionType> RoadSesionTypes { get; set; }

    public virtual DbSet<RoadType> RoadTypes { get; set; }

    public virtual DbSet<RoadwayIssue> RoadwayIssues { get; set; }

    public virtual DbSet<RoadwayIssuesType> RoadwayIssuesTypes { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<RoleCommand> RoleCommands { get; set; }

    public virtual DbSet<RoleDetail> RoleDetails { get; set; }

    public virtual DbSet<SubSystem> SubSystems { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=HoaBanManager;User=root;Password=;");
        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CameraRecord>(entity =>
        {
            entity.ToTable("CameraRecord");

            entity.HasIndex(e => e.CameraId, "UQ_CameraRecord_CameraID").IsUnique();

            entity.Property(e => e.AccessToken)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.CameraCode)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CameraId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CameraID");
            entity.Property(e => e.CameraName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ChainageFrom)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasPrecision(3);
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Location)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedDate).HasPrecision(3);
            entity.Property(e => e.ServerAddress)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<GoverningBody>(entity =>
        {
            entity.ToTable("GoverningBody");

            entity.HasIndex(e => e.GoverningCode, "UQ_GoverningBody_GoverningCode").IsUnique();

            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasPrecision(3);
            entity.Property(e => e.DienGiai).HasMaxLength(255);
            entity.Property(e => e.GoverningCode)
                .HasMaxLength(60)
                .IsUnicode(false);
            entity.Property(e => e.GoverningName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedDate).HasPrecision(3);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Personnel>(entity =>
        {
            entity.HasIndex(e => e.PersonelCode, "UQ_Personnel_PersonelCode").IsUnique();

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreatedDate).HasPrecision(3);
            entity.Property(e => e.Department)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.EmploymentType)
                .HasMaxLength(60)
                .IsUnicode(false);
            entity.Property(e => e.FullName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.MobilePhoneNumber)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedDate).HasPrecision(3);
            entity.Property(e => e.PersonelCode)
                .HasMaxLength(60)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Road>(entity =>
        {
            entity.ToTable("Road");

            entity.HasIndex(e => e.RoadCode, "UQ_Road_RoadCode").IsUnique();

            entity.Property(e => e.ChainageFrom)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.ChainageTo)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasPrecision(3);
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.EndGps)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("EndGPS");
            entity.Property(e => e.ModifiedDate).HasPrecision(3);
            entity.Property(e => e.RoadCode)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.RoadName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.StartGps)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("StartGPS");

            entity.HasOne(d => d.Parent).WithMany(p => p.InverseParent)
                .HasForeignKey(d => d.ParentId)
                .HasConstraintName("FK_Road_Road");

            entity.HasOne(d => d.Type).WithMany(p => p.Roads)
                .HasForeignKey(d => d.TypeId)
                .HasConstraintName("FK_Road_RoadType");
        });

        modelBuilder.Entity<RoadInfrastructure>(entity =>
        {
            entity.ToTable("RoadInfrastructure");

            entity.HasIndex(e => e.InfrastructureCode, "UQ_RoadInfrastructure_AssetCode").IsUnique();

            entity.Property(e => e.ChainageFrom)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasPrecision(3);
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.InfrastructureCode)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.InfrastructureName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.InfrastructureType)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Location)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedDate).HasPrecision(3);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<RoadSection>(entity =>
        {
            entity.ToTable("RoadSection");

            entity.HasIndex(e => e.RoadSessionCode, "UQ_RoadSection_RoadSessionCode").IsUnique();

            entity.Property(e => e.ChainageFrom)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.ChainageTo)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasPrecision(3);
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.EndGps)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("EndGPS");
            entity.Property(e => e.ModifiedDate).HasPrecision(3);
            entity.Property(e => e.RoadSessionCode)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.RoadSessionName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.StartGps)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("StartGPS");

            entity.HasOne(d => d.Road).WithMany(p => p.RoadSections)
                .HasForeignKey(d => d.RoadId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RoadSection_Road");

            entity.HasOne(d => d.SessionType).WithMany(p => p.RoadSections)
                .HasForeignKey(d => d.SessionTypeId)
                .HasConstraintName("FK_RoadSection_RoadSesionType");
        });

        modelBuilder.Entity<RoadSesionInfrastructure>(entity =>
        {
            entity.HasKey(e => new { e.RoadSesionId, e.InfrashtructureId });

            entity.ToTable("RoadSesionInfrastructure");
        });

        modelBuilder.Entity<RoadSesionType>(entity =>
        {
            entity.ToTable("RoadSesionType");

            entity.HasIndex(e => e.TypeCode, "UQ_RoadSesionType_TypeCode").IsUnique();

            entity.Property(e => e.CreatedDate).HasPrecision(3);
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedDate).HasPrecision(3);
            entity.Property(e => e.TypeCode)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.TypeName)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<RoadType>(entity =>
        {
            entity.ToTable("RoadType");

            entity.HasIndex(e => e.TypeCode, "UQ_RoadType_TypeCode").IsUnique();

            entity.Property(e => e.CreatedDate).HasPrecision(3);
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedName).HasPrecision(3);
            entity.Property(e => e.TypeCode)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.TypeName)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<RoadwayIssue>(entity =>
        {
            entity.ToTable("RoadwayIssue");

            entity.HasIndex(e => e.TypeCode, "UQ_RoadwayIssue_TypeCode").IsUnique();

            entity.Property(e => e.TypeCode)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<RoadwayIssuesType>(entity =>
        {
            entity.ToTable("RoadwayIssuesType");

            entity.HasIndex(e => e.TypeCode, "UQ_RoadwayIssuesType_TypeCode").IsUnique();

            entity.Property(e => e.CreatedDate).HasPrecision(3);
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedDate).HasPrecision(3);
            entity.Property(e => e.TypeCode)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.TypeName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("Role");

            entity.HasIndex(e => e.RoleCode, "UQ_Role_RoleCode").IsUnique();

            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.RoleCode)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.RoleName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<RoleCommand>(entity =>
        {
            entity.ToTable("RoleCommand");

            entity.HasIndex(e => e.CommandCode, "UQ_RoleCommand_CommandCode").IsUnique();

            entity.Property(e => e.CommandCode)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.CommandName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<RoleDetail>(entity =>
        {
            entity.ToTable("RoleDetail");

            entity.HasOne(d => d.Command).WithMany(p => p.RoleDetails)
                .HasForeignKey(d => d.CommandId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RoleDetail_RoleCommand");

            entity.HasOne(d => d.Role).WithMany(p => p.RoleDetails)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RoleDetail_Role");

            entity.HasOne(d => d.SubSystem).WithMany(p => p.RoleDetails)
                .HasForeignKey(d => d.SubSystemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RoleDetail_SubSystem");
        });

        modelBuilder.Entity<SubSystem>(entity =>
        {
            entity.ToTable("SubSystem");

            entity.HasIndex(e => e.SubSystemCode, "UQ_SubSystem_SubSystemCode").IsUnique();

            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.SubSystemCode)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.SubSystemName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.HasIndex(e => e.UserName, "UQ_User_UserName").IsUnique();

            entity.Property(e => e.EmailAddress)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.FullName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.MobileNumber)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UserName)
                .HasMaxLength(60)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
