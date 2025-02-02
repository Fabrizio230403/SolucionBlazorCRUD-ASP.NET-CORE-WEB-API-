﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

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

    public virtual DbSet<Notificacione> Notificaciones { get; set; }

    public virtual DbSet<Proyecto> Proyectos { get; set; }

    public virtual DbSet<Recurso> Recursos { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<RolesPermiso> Roles_Permisos { get; set; }

    public virtual DbSet<Permiso> Permiso { get; set; }

    public virtual DbSet<Tarea> Tareas { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Notificacione>(entity =>
        {
            entity.HasKey(e => e.NotificacionId).HasName("PK__Notifica__BCC120C47D63A637");

            entity.Property(e => e.NotificacionId).HasColumnName("NotificacionID");
            entity.Property(e => e.Estado)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValue("P")
                .IsFixedLength();
            entity.Property(e => e.FechaEnvio)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Mensaje).HasMaxLength(255);
            entity.Property(e => e.Tipo)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Notificaciones)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("FK__Notificac__Usuar__75A278F5");
        });

        modelBuilder.Entity<Proyecto>(entity =>
        {
            entity.HasKey(e => e.ProyectoId).HasName("PK__Proyecto__CF241D458F95C068");

            entity.Property(e => e.ProyectoId).HasColumnName("ProyectoID");
            entity.Property(e => e.Estado)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.GerenteId).HasColumnName("GerenteID");
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.PorcentajeCompleto)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(5, 2)");
            entity.Property(e => e.Prioridad)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.Gerente).WithMany(p => p.Proyectos)
                .HasForeignKey(d => d.GerenteId)
                .HasConstraintName("FK__Proyectos__Geren__66603565");
        });

        modelBuilder.Entity<Recurso>(entity =>
        {
            entity.HasKey(e => e.RecursoId).HasName("PK__Recursos__82F2B1A4B7F9A9A3");

            entity.Property(e => e.RecursoId).HasColumnName("RecursoID");
            entity.Property(e => e.Costo)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.ProyectoId).HasColumnName("ProyectoID");
            entity.Property(e => e.TiempoAsignado)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(5, 2)");
            entity.Property(e => e.Tipo)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");

            entity.HasOne(d => d.Proyecto).WithMany(p => p.Recursos)
                .HasForeignKey(d => d.ProyectoId)
                .HasConstraintName("FK__Recursos__Proyec__6EF57B66");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Recursos)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("FK__Recursos__Usuari__72C60C4A");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RolId).HasName("PK__Roles__F92302D1726DA4E5");

            entity.Property(e => e.RolId).HasColumnName("RolID");
            entity.Property(e => e.Descripcion).HasMaxLength(200);
            entity.Property(e => e.Nombre).HasMaxLength(50);
        });

        modelBuilder.Entity<Tarea>(entity =>
        {
            entity.HasKey(e => e.TareaId).HasName("PK__Tareas__5CD83671ACDEFFE1");

            entity.Property(e => e.TareaId).HasColumnName("TareaID");
            entity.Property(e => e.Estado)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.ProyectoId).HasColumnName("ProyectoID");
            entity.Property(e => e.UsuarioAsignadoId).HasColumnName("UsuarioAsignadoID");

            entity.HasOne(d => d.Proyecto).WithMany(p => p.Tareas)
                .HasForeignKey(d => d.ProyectoId)
                .HasConstraintName("FK__Tareas__Proyecto__6A30C649");

            entity.HasOne(d => d.UsuarioAsignado).WithMany(p => p.Tareas)
                .HasForeignKey(d => d.UsuarioAsignadoId)
                .HasConstraintName("FK__Tareas__UsuarioA__6C190EBB");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.UsuarioId).HasName("PK__Usuarios__2B3DE798A0CD787A");

            entity.HasIndex(e => e.Email, "UQ__Usuarios__A9D10534B1C4C3E1").IsUnique();

            entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FechaExpiracionToken).HasColumnType("datetime");
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.RolId).HasColumnName("RolID");

            entity.HasOne(d => d.Rol).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.RolId)
                .HasConstraintName("FK__Usuarios__RolID__60A75C0F");
        });

        modelBuilder.Entity<Permiso>(entity =>
        {
            entity.ToTable("Permisos");

            entity.HasKey(e => e.PermisoId);
            entity.Property(e => e.Nombre).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Descripcion).HasMaxLength(250);
        });

        modelBuilder.Entity<RolesPermiso>(entity =>
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

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
