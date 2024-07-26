using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace RoadTrafficManagement.Models;

public partial class BaotrithuongxuyenContext : DbContext
{
    public BaotrithuongxuyenContext()
    {
    }

    public BaotrithuongxuyenContext(DbContextOptions<BaotrithuongxuyenContext> options)
        : base(options)
    {
    }

    public virtual DbSet<GoverningBody> GoverningBodies { get; set; }

    public virtual DbSet<Personnel> Personnel { get; set; }

    public virtual DbSet<Road> Roads { get; set; }

    public virtual DbSet<RoadGuardrail> RoadGuardrails { get; set; }

    public virtual DbSet<RoadInfrastructure> RoadInfrastructures { get; set; }

    public virtual DbSet<RoadProperty> RoadProperties { get; set; }

    public virtual DbSet<RoadSection> RoadSections { get; set; }

    public virtual DbSet<RoadSesionType> RoadSesionTypes { get; set; }

    public virtual DbSet<RoadType> RoadTypes { get; set; }

    public virtual DbSet<RoadwayIssue> RoadwayIssues { get; set; }

    public virtual DbSet<RoadwayIssuesType> RoadwayIssuesTypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<GoverningBody>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Governin__3214EC073CD0BC28");

            entity.ToTable("GoverningBody");

            entity.HasIndex(e => e.GoverningCode, "UQ__Governin__2C9BAF45C03FB950").IsUnique();

            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.GoverningCode).HasMaxLength(60);
            entity.Property(e => e.GoverningName).HasMaxLength(255);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.PhoneNumber).HasMaxLength(20);
        });

        modelBuilder.Entity<Personnel>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Personne__3214EC073A703725");

            entity.HasIndex(e => e.PersonelCode, "UQ__Personne__9BED17A1F62AE6E0").IsUnique();

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Department).HasMaxLength(20);
            entity.Property(e => e.EmploymentType).HasMaxLength(60);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.MobilePhoneNumber).HasMaxLength(20);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.PersonelCode).HasMaxLength(60);
        });

        modelBuilder.Entity<Road>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Road__3214EC07A07A0C4C");

            entity.ToTable("Road");

            entity.HasIndex(e => e.RoadCode, "UQ__Road__B73CF2E183294D72").IsUnique();

            entity.Property(e => e.ChainageFrom).HasMaxLength(30);
            entity.Property(e => e.ChainageTo).HasMaxLength(30);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.EndGps)
                .HasMaxLength(100)
                .HasColumnName("EndGPS");
            entity.Property(e => e.KilometerEnd).HasColumnType("decimal(20, 6)");
            entity.Property(e => e.KilometerStart).HasColumnType("decimal(20, 6)");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.RoadCode).HasMaxLength(100);
            entity.Property(e => e.RoadName).HasMaxLength(255);
            entity.Property(e => e.StartGps)
                .HasMaxLength(100)
                .HasColumnName("StartGPS");

            entity.HasOne(d => d.Parent).WithMany(p => p.InverseParent)
                .HasForeignKey(d => d.ParentId)
                .HasConstraintName("FK__Road__ParentId__1ED998B2");

            entity.HasOne(d => d.Type).WithMany(p => p.Roads)
                .HasForeignKey(d => d.TypeId)
                .HasConstraintName("FK__Road__TypeId__1FCDBCEB");
        });

        modelBuilder.Entity<RoadGuardrail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__RoadGuar__3214EC0797997CEA");

            entity.ToTable("RoadGuardrail");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.KilometerEnd).HasColumnType("decimal(20, 6)");
            entity.Property(e => e.KilometerStart).HasColumnType("decimal(20, 6)");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(255);

            entity.HasOne(d => d.Road).WithMany(p => p.RoadGuardrails)
                .HasForeignKey(d => d.RoadId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RoadGuard__RoadI__22AA2996");
        });

        modelBuilder.Entity<RoadInfrastructure>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__RoadInfr__3214EC074108EA90");

            entity.ToTable("RoadInfrastructure");

            entity.Property(e => e.Chainage).HasMaxLength(50);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.InstallationDate).HasColumnType("datetime");
            entity.Property(e => e.Kilometer).HasColumnType("decimal(20, 6)");
            entity.Property(e => e.Location).HasMaxLength(50);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(50);

            entity.HasOne(d => d.Property).WithMany(p => p.RoadInfrastructures)
                .HasForeignKey(d => d.PropertyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RoadInfra__Prope__2B3F6F97");

            entity.HasOne(d => d.Road).WithMany(p => p.RoadInfrastructures)
                .HasForeignKey(d => d.RoadId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RoadInfra__RoadI__2C3393D0");
        });

        modelBuilder.Entity<RoadProperty>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__RoadProp__3214EC074EFDB695");

            entity.ToTable("RoadProperty");

            entity.HasIndex(e => e.PropertyCode, "UQ__RoadProp__9CF1543A74E4FDE6").IsUnique();

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.PropertyCode).HasMaxLength(100);
            entity.Property(e => e.PropertyName).HasMaxLength(255);
        });

        modelBuilder.Entity<RoadSection>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__RoadSect__3214EC07CCD9912D");

            entity.ToTable("RoadSection");

            entity.HasIndex(e => e.RoadSessionCode, "UQ__RoadSect__289D1131E3FBEBF1").IsUnique();

            entity.Property(e => e.ChainageFrom).HasMaxLength(30);
            entity.Property(e => e.ChainageTo).HasMaxLength(30);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.EndGps)
                .HasMaxLength(100)
                .HasColumnName("EndGPS");
            entity.Property(e => e.KilometerEnd)
                .HasDefaultValue(0.000000m)
                .HasColumnType("decimal(20, 6)");
            entity.Property(e => e.KilometerStart)
                .HasDefaultValue(0.000000m)
                .HasColumnType("decimal(20, 6)");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.RoadSessionCode).HasMaxLength(100);
            entity.Property(e => e.RoadSessionName).HasMaxLength(255);
            entity.Property(e => e.StartGps)
                .HasMaxLength(100)
                .HasColumnName("StartGPS");

            entity.HasOne(d => d.Road).WithMany(p => p.RoadSections)
                .HasForeignKey(d => d.RoadId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RoadSecti__RoadI__34C8D9D1");

            entity.HasOne(d => d.SessionType).WithMany(p => p.RoadSections)
                .HasForeignKey(d => d.SessionTypeId)
                .HasConstraintName("FK__RoadSecti__Sessi__35BCFE0A");
        });

        modelBuilder.Entity<RoadSesionType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__RoadSesi__3214EC07704C3A38");

            entity.ToTable("RoadSesionType");

            entity.HasIndex(e => e.TypeCode, "UQ__RoadSesi__3E1CDC7CA372DE35").IsUnique();

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.TypeCode).HasMaxLength(100);
            entity.Property(e => e.TypeName).HasMaxLength(255);
        });

        modelBuilder.Entity<RoadType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__RoadType__3214EC07DB6E7C79");

            entity.ToTable("RoadType");

            entity.HasIndex(e => e.TypeCode, "UQ__RoadType__3E1CDC7CEF8B3DDB").IsUnique();

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.TypeCode).HasMaxLength(100);
            entity.Property(e => e.TypeName).HasMaxLength(255);
        });

        modelBuilder.Entity<RoadwayIssue>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__RoadwayI__3214EC076B448829");

            entity.ToTable("RoadwayIssue");

            entity.HasIndex(e => e.IssueCode, "UQ__RoadwayI__1CF9DA763907CC2B").IsUnique();

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.IssueCode).HasMaxLength(100);
            entity.Property(e => e.IssueDate).HasColumnType("datetime");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

            entity.HasOne(d => d.Type).WithMany(p => p.RoadwayIssues)
                .HasForeignKey(d => d.TypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RoadwayIs__TypeI__403A8C7D");
        });

        modelBuilder.Entity<RoadwayIssuesType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__RoadwayI__3214EC07510940FD");

            entity.ToTable("RoadwayIssuesType");

            entity.HasIndex(e => e.TypeCode, "UQ__RoadwayI__3E1CDC7C42436D9C").IsUnique();

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.TypeCode).HasMaxLength(100);
            entity.Property(e => e.TypeName).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
