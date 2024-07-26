﻿using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace RoadTrafficManagement.AppModels;

public partial class QuanLyGiaoThongContext : IdentityDbContext<ApplicationUser>
{
    public QuanLyGiaoThongContext()
    {
    }

    public QuanLyGiaoThongContext(DbContextOptions<QuanLyGiaoThongContext> options)
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
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseMySql("Server=localhost;Database=QuanLyGiaoThong;User=root;Password=;",
                new MySqlServerVersion(new Version(8, 0, 30)));
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<GoverningBody>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("GoverningBody", tb => tb.HasComment("Đơn vị chủ quản"));

            entity.HasIndex(e => e.GoverningCode, "GoverningCode").IsUnique();

            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .HasComment("Dia chi");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime(3)");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasComment("Dien giai");
            entity.Property(e => e.GoverningCode)
                .HasMaxLength(60)
                .HasComment("Ma don vi");
            entity.Property(e => e.GoverningName)
                .HasMaxLength(255)
                .HasComment("Ten don vi");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime(3)");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .HasComment("Dien thoai");
        });

        modelBuilder.Entity<Personnel>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.HasIndex(e => e.PersonelCode, "PersonelCode").IsUnique();

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreatedDate).HasColumnType("datetime(3)");
            entity.Property(e => e.Department).HasMaxLength(20);
            entity.Property(e => e.EmploymentType).HasMaxLength(60);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.MobilePhoneNumber).HasMaxLength(20);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime(3)");
            entity.Property(e => e.PersonelCode).HasMaxLength(60);
        });

        modelBuilder.Entity<Road>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("Road", tb => tb.HasComment("Tuyen duong thong tin chung"));

            entity.HasIndex(e => e.ParentId, "FK_Road_Road");

            entity.HasIndex(e => e.TypeId, "FK_Road_RoadType");

            entity.HasIndex(e => e.RoadCode, "RoadCode").IsUnique();

            entity.Property(e => e.ChainageFrom)
                .HasMaxLength(30)
                .HasComment("Diem bat dau (km)");
            entity.Property(e => e.ChainageTo)
                .HasMaxLength(30)
                .HasComment("Diem ket thuc (km)");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime(3)");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.EndGps)
                .HasMaxLength(100)
                .HasComment("Toa do GPS ket thuc")
                .HasColumnName("EndGPS");
            entity.Property(e => e.KilometerEnd).HasColumnType("decimal(20,6) unsigned");
            entity.Property(e => e.KilometerStart).HasColumnType("decimal(20,6) unsigned");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime(3)");
            entity.Property(e => e.RoadCode)
                .HasMaxLength(100)
                .HasComment("Ma duong");
            entity.Property(e => e.RoadName)
                .HasMaxLength(255)
                .HasComment("Ten duong");
            entity.Property(e => e.StartGps)
                .HasMaxLength(100)
                .HasComment("Toa do GPS bat dau")
                .HasColumnName("StartGPS");

            entity.HasOne(d => d.Parent).WithMany(p => p.InverseParent)
                .HasForeignKey(d => d.ParentId)
                .HasConstraintName("FK_Road_Road");

            entity.HasOne(d => d.Type).WithMany(p => p.Roads)
                .HasForeignKey(d => d.TypeId)
                .HasConstraintName("FK_Road_RoadType");
        });

        modelBuilder.Entity<RoadGuardrail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("RoadGuardrail", tb => tb.HasComment("Ho lan"));

            entity.HasIndex(e => e.RoadId, "FK_Guardrail_Road");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime(3)");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.KilometerEnd).HasPrecision(20, 6);
            entity.Property(e => e.KilometerStart).HasPrecision(20, 6);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime(3)");
            entity.Property(e => e.Status).HasMaxLength(255);

            entity.HasOne(d => d.Road).WithMany(p => p.RoadGuardrails)
                .HasForeignKey(d => d.RoadId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Guardrail_Road");
        });

        modelBuilder.Entity<RoadInfrastructure>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("RoadInfrastructure", tb => tb.HasComment("Co so ha tang"));

            entity.HasIndex(e => e.RoadId, "FK_RoadInfrastructure_Road");

            entity.HasIndex(e => e.PropertyId, "FK_RoadInfrastructure_RoadProperty");

            entity.Property(e => e.Chainage).HasMaxLength(50);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime(3)");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.InstallationDate).HasColumnType("datetime");
            entity.Property(e => e.Kilometer).HasColumnType("decimal(20,6) unsigned");
            entity.Property(e => e.Location).HasMaxLength(50);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime(3)");
            entity.Property(e => e.Status).HasMaxLength(50);

            entity.HasOne(d => d.Property).WithMany(p => p.RoadInfrastructures)
                .HasForeignKey(d => d.PropertyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RoadInfrastructure_RoadProperty");

            entity.HasOne(d => d.Road).WithMany(p => p.RoadInfrastructures)
                .HasForeignKey(d => d.RoadId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RoadInfrastructure_Road");
        });

        modelBuilder.Entity<RoadProperty>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("RoadProperty", tb => tb.HasComment("Tai san thuoc tinh"));

            entity.HasIndex(e => e.PropertyCode, "ClassCode").IsUnique();

            entity.Property(e => e.CreatedDate).HasColumnType("datetime(3)");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime(3)");
            entity.Property(e => e.PropertyCode).HasMaxLength(100);
            entity.Property(e => e.PropertyName).HasMaxLength(255);
        });

        modelBuilder.Entity<RoadSection>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("RoadSection", tb => tb.HasComment("Tuyen duong quan ly"));

            entity.HasIndex(e => e.RoadId, "FK_RoadSection_Road");

            entity.HasIndex(e => e.SessionTypeId, "FK_RoadSection_RoadSesionType");

            entity.HasIndex(e => e.RoadSessionCode, "RoadSessionCode").IsUnique();

            entity.Property(e => e.ChainageFrom)
                .HasMaxLength(30)
                .HasComment("Ly trinh bat dau");
            entity.Property(e => e.ChainageTo)
                .HasMaxLength(30)
                .HasComment("Ly trinh ket thuc");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime(3)");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.EndGps)
                .HasMaxLength(100)
                .HasColumnName("EndGPS");
            entity.Property(e => e.KilometerEnd)
                .HasPrecision(20, 6)
                .HasDefaultValueSql("'0.000000'");
            entity.Property(e => e.KilometerStart)
                .HasPrecision(20, 6)
                .HasDefaultValueSql("'0.000000'");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime(3)");
            entity.Property(e => e.RoadSessionCode)
                .HasMaxLength(100)
                .HasComment("Ma tuyen");
            entity.Property(e => e.RoadSessionName)
                .HasMaxLength(255)
                .HasComment("Ten tuyen");
            entity.Property(e => e.StartGps)
                .HasMaxLength(100)
                .HasComment("GPS bat dau")
                .HasColumnName("StartGPS");

            entity.HasOne(d => d.Road).WithMany(p => p.RoadSections)
                .HasForeignKey(d => d.RoadId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RoadSection_Road");

            entity.HasOne(d => d.SessionType).WithMany(p => p.RoadSections)
                .HasForeignKey(d => d.SessionTypeId)
                .HasConstraintName("FK_RoadSection_RoadSesionType");
        });

        modelBuilder.Entity<RoadSesionType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("RoadSesionType", tb => tb.HasComment("Loai tuyen duong quan ly"));

            entity.HasIndex(e => e.TypeCode, "TypeCode").IsUnique();

            entity.Property(e => e.CreatedDate).HasColumnType("datetime(3)");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime(3)");
            entity.Property(e => e.TypeCode).HasMaxLength(100);
            entity.Property(e => e.TypeName).HasMaxLength(255);
        });

        modelBuilder.Entity<RoadType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("RoadType", tb => tb.HasComment("Loai duong cho tuyen duong thong tin chung"));

            entity.HasIndex(e => e.TypeCode, "TypeCode").IsUnique();

            entity.Property(e => e.CreatedDate).HasColumnType("datetime(3)");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasComment("Dien giai");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime(3)");
            entity.Property(e => e.TypeCode)
                .HasMaxLength(100)
                .HasComment("Ma loai");
            entity.Property(e => e.TypeName)
                .HasMaxLength(255)
                .HasComment("Ten loai");
        });

        modelBuilder.Entity<RoadwayIssue>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("RoadwayIssue", tb => tb.HasComment("Su co tuyen duong quan ly"));

            entity.HasIndex(e => e.TypeId, "FK_RoadwayIssue_RoadwayIssuesType");

            entity.HasIndex(e => e.IssueCode, "TypeCode").IsUnique();

            entity.Property(e => e.CreatedDate).HasColumnType("datetime(3)");
            entity.Property(e => e.Description).HasColumnType("mediumtext");
            entity.Property(e => e.IssueDate).HasColumnType("datetime");
            entity.Property(e => e.IssueName).HasColumnType("mediumtext");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime(3)");

            entity.HasOne(d => d.Type).WithMany(p => p.RoadwayIssues)
                .HasForeignKey(d => d.TypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RoadwayIssue_RoadwayIssuesType");
        });

        modelBuilder.Entity<RoadwayIssuesType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("RoadwayIssuesType");

            entity.HasIndex(e => e.TypeCode, "TypeCode").IsUnique();

            entity.Property(e => e.CreatedDate).HasColumnType("datetime(3)");
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime(3)");
            entity.Property(e => e.TypeCode).HasMaxLength(100);
            entity.Property(e => e.TypeName).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
