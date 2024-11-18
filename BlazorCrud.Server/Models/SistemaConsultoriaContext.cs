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

    public virtual DbSet<Notificacione> Notificaciones { get; set; }

    public virtual DbSet<Proyecto> Proyectos { get; set; }

    public virtual DbSet<Recurso> Recursos { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Tarea> Tareas { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<Permiso> Permiso { get; set; }

    public virtual DbSet<RolPermiso> Roles_Permisos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Notificacione>(entity =>
        {
            entity.HasKey(e => e.NotificacionId).HasName("PK__Notifica__BCC120C4B56191C1");

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
                .HasConstraintName("FK__Notificac__Usuar__74AE54BC");
        });

        modelBuilder.Entity<Recurso>(entity =>
        {
            entity.HasKey(e => e.RecursoId).HasName("PK__Recursos__82F2B1A415804F64");

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
                .HasConstraintName("FK__Recursos__Proyec__6E01572D");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Recursos)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("FK__Recursos__Usuari__71D1E811");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RolId).HasName("PK__Roles__F92302D18AE865EF");

            entity.Property(e => e.RolId).HasColumnName("RolID");
            entity.Property(e => e.Descripcion).HasMaxLength(200);
            entity.Property(e => e.Nombre).HasMaxLength(50);
        });

        modelBuilder.Entity<Proyecto>(entity =>
        {
            entity.HasKey(e => e.ProyectoID).HasName("PK__Proyecto__CF241D452E3166F6");
            entity.Property(e => e.ProyectoID).HasColumnName("ProyectoID");
            entity.Property(e => e.Nombre).HasMaxLength(100).IsRequired();
            entity.Property(e => e.Descripcion).HasMaxLength(500);
            entity.Property(e => e.Prioridad).HasMaxLength(1).IsUnicode(false).IsFixedLength();
            entity.Property(e => e.Estado).HasMaxLength(1).IsUnicode(false).IsFixedLength();
            entity.Property(e => e.GerenteID).HasColumnName("GerenteID");
            entity.Property(e => e.PorcentajeCompleto).HasDefaultValue(0m).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.FechaInicio).HasColumnType("date");
            entity.Property(e => e.FechaFin).HasColumnType("date");
            entity.HasOne(d => d.Gerente).WithMany(p => p.Proyectos).HasForeignKey(d => d.GerenteID).HasConstraintName("FK__Proyectos__Geren__656C112C");
        });



        modelBuilder.Entity<Tarea>(entity =>
        {
            entity.HasKey(e => e.TareaID).HasName("PK__Tareas__5CD836718CF56CB4");
            entity.Property(e => e.TareaID).HasColumnName("TareaID");
            entity.Property(e => e.Nombre).HasMaxLength(100).IsRequired();
            entity.Property(e => e.Descripcion).IsRequired(false);
            entity.Property(e => e.FechaInicio).HasColumnType("date").IsRequired(false);
            entity.Property(e => e.FechaFin).HasColumnType("date").IsRequired(false);
            entity.Property(e => e.Estado).HasMaxLength(50).IsUnicode(false).IsRequired(false);
            entity.HasOne(d => d.Proyecto).WithMany(p => p.Tareas).HasForeignKey(d => d.ProyectoID).HasConstraintName("FK_Tareas_ProyectoID");
            entity.HasOne(d => d.UsuarioAsignado).WithMany(p => p.Tareas).HasForeignKey(d => d.UsuarioAsignadoID).HasConstraintName("FK_Tareas_UsuarioAsignadoID");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.UsuarioId).HasName("PK__Usuarios__2B3DE798D6BF9814");

            entity.HasIndex(e => e.Email, "UQ__Usuarios__A9D1053413FA8D7B").IsUnique();

            entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.RolId).HasColumnName("RolID");

            entity.HasOne(d => d.Rol).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.RolId)
                .HasConstraintName("FK__Usuarios__RolID__5FB337D6");
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