using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using BlazorCrud.Server.Models;

namespace BlazorCrud.Server.Models;

public partial class SistemaConsultoriaContext : DbContext
{
    public SistemaConsultoriaContext()
    {
    }

    public SistemaConsultoriaContext(DbContextOptions<SistemaConsultoriaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Proyecto> Proyectos { get; set; }
    public virtual DbSet<Tarea> Tarea { get; set; }

    public virtual DbSet<Permiso> Permiso { get; set; }

    public virtual DbSet<RolPermiso> Roles_Permisos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Proyecto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__proyecto__3214EC07A6E4C763");
            entity.ToTable("proyecto");
            entity.Property(e => e.Descripcion).HasMaxLength(500);
            entity.Property(e => e.Estado).HasMaxLength(1).IsUnicode(false).IsFixedLength();
            entity.Property(e => e.FechaActualizacion).HasColumnType("datetime");
            entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(getdate())").HasColumnType("datetime");
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.PresupuestoEstimado).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Prioridad).HasMaxLength(1).IsUnicode(false).IsFixedLength();
            entity.Property(e => e.RecursosAsignados).HasMaxLength(500);
        });

        modelBuilder.Entity<Tarea>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tarea__3214EC07A6E4C763");
            entity.ToTable("tarea");
            //entity.Property(e => e.Nombre).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Descripcion).HasMaxLength(500);
            entity.Property(e => e.FechaInicio).IsRequired().HasColumnType("date");
            entity.Property(e => e.FechaFin).HasColumnType("date");
            entity.Property(e => e.Prioridad).HasMaxLength(1).IsUnicode(false).IsFixedLength();
            entity.Property(e => e.Estado).HasMaxLength(1).IsUnicode(false).IsFixedLength();
            entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(getdate())").HasColumnType("datetime");
            entity.Property(e => e.FechaActualizacion).HasColumnType("datetime");
            entity.Property(e => e.ProyectoId).HasColumnName("ProyectoId");
        });

        modelBuilder.Entity<Permiso>(entity =>
        {
            entity.ToTable("Permisos");

            entity.HasKey(e => e.PermisoID);
            entity.Property(e => e.Nombre).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Descripcion).HasMaxLength(250);
        });

        modelBuilder.Entity<RolPermiso>(entity =>
        {
            entity.HasKey(e => new { e.RolId, e.PermisoId }).HasName("PK_Roles_Permisos");

            entity.Property(e => e.RolId).HasColumnName("RolID");
            entity.Property(e => e.PermisoId).HasColumnName("PermisoID");

            entity.HasOne(d => d.Rol)
                .WithMany(p => p.RolPermisos)
                .HasForeignKey(d => d.RolId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Roles_Permisos_Rol");

            entity.HasOne(d => d.Permiso)
                .WithMany(p => p.RolPermisos)
                .HasForeignKey(d => d.PermisoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Roles_Permisos_Permiso");
        });

        base.OnModelCreating(modelBuilder);

    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);


}