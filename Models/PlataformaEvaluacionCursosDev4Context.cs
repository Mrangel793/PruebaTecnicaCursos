using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PruebaCursos.Models;

public partial class PlataformaEvaluacionCursosDev4Context : DbContext
{
    public PlataformaEvaluacionCursosDev4Context()
    {
    }

    public PlataformaEvaluacionCursosDev4Context(DbContextOptions<PlataformaEvaluacionCursosDev4Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Curso> Cursos { get; set; }

    public virtual DbSet<Evaluacione> Evaluaciones { get; set; }

    public virtual DbSet<Inscripcione> Inscripciones { get; set; }

    public virtual DbSet<Intento> Intentos { get; set; }

    public virtual DbSet<Pregunta> Preguntas { get; set; }

    public virtual DbSet<Respuesta> Respuestas { get; set; }

    public virtual DbSet<RespuestasUsuario> RespuestasUsuarios { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<UsuarioRole> UsuarioRoles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=TECNOPARQUE\\SQLEXPRESS;Database=PlataformaEvaluacionCursosDev4;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Curso>(entity =>
        {
            entity.HasKey(e => e.CursoId).HasName("PK__Cursos__7E023A37B9FB8A43");

            entity.Property(e => e.CursoId).HasColumnName("CursoID");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Nombre).HasMaxLength(100);
        });

        modelBuilder.Entity<Evaluacione>(entity =>
        {
            entity.HasKey(e => e.EvaluacionId).HasName("PK__Evaluaci__99ABA8A57E6AB9EB");

            entity.Property(e => e.EvaluacionId).HasColumnName("EvaluacionID");
            entity.Property(e => e.CursoId).HasColumnName("CursoID");
            entity.Property(e => e.FechaFin).HasColumnType("datetime");
            entity.Property(e => e.FechaInicio).HasColumnType("datetime");
            entity.Property(e => e.Titulo).HasMaxLength(100);

            entity.HasOne(d => d.Curso).WithMany(p => p.Evaluaciones)
                .HasForeignKey(d => d.CursoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Evaluacio__Curso__49C3F6B7");
        });

        modelBuilder.Entity<Inscripcione>(entity =>
        {
            entity.HasKey(e => e.InscripcionId).HasName("PK__Inscripc__168316999C3663E7");

            entity.Property(e => e.InscripcionId).HasColumnName("InscripcionID");
            entity.Property(e => e.CursoId).HasColumnName("CursoID");
            entity.Property(e => e.FechaInscripcion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");

            entity.HasOne(d => d.Curso).WithMany(p => p.Inscripciones)
                .HasForeignKey(d => d.CursoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Inscripci__Curso__45F365D3");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Inscripciones)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Inscripci__Usuar__44FF419A");
        });

        modelBuilder.Entity<Intento>(entity =>
        {
            entity.HasKey(e => e.IntentoId).HasName("PK__Intentos__F1BF51CE7D2CF4A7");

            entity.Property(e => e.IntentoId).HasColumnName("IntentoID");
            entity.Property(e => e.EvaluacionId).HasColumnName("EvaluacionID");
            entity.Property(e => e.FechaFin).HasColumnType("datetime");
            entity.Property(e => e.FechaInicio).HasColumnType("datetime");
            entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");

            entity.HasOne(d => d.Evaluacion).WithMany(p => p.Intentos)
                .HasForeignKey(d => d.EvaluacionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Intentos__Evalua__534D60F1");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Intentos)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Intentos__Usuari__52593CB8");
        });

        modelBuilder.Entity<Pregunta>(entity =>
        {
            entity.HasKey(e => e.PreguntaId).HasName("PK__Pregunta__EBB2A35950478DBB");

            entity.Property(e => e.PreguntaId).HasColumnName("PreguntaID");
            entity.Property(e => e.EvaluacionId).HasColumnName("EvaluacionID");
            entity.Property(e => e.Tipo).HasMaxLength(50);

            entity.HasOne(d => d.Evaluacion).WithMany(p => p.Pregunta)
                .HasForeignKey(d => d.EvaluacionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Preguntas__Evalu__4CA06362");
        });

        modelBuilder.Entity<Respuesta>(entity =>
        {
            entity.HasKey(e => e.RespuestaId).HasName("PK__Respuest__31F7FC3182CBD120");

            entity.Property(e => e.RespuestaId).HasColumnName("RespuestaID");
            entity.Property(e => e.PreguntaId).HasColumnName("PreguntaID");

            entity.HasOne(d => d.Pregunta).WithMany(p => p.Respuesta)
                .HasForeignKey(d => d.PreguntaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Respuesta__Pregu__4F7CD00D");
        });

        modelBuilder.Entity<RespuestasUsuario>(entity =>
        {
            entity.HasKey(e => e.RespuestaUsuarioId).HasName("PK__Respuest__55B8B8D24F0A9392");

            entity.ToTable("RespuestasUsuario");

            entity.Property(e => e.RespuestaUsuarioId).HasColumnName("RespuestaUsuarioID");
            entity.Property(e => e.IntentoId).HasColumnName("IntentoID");
            entity.Property(e => e.PreguntaId).HasColumnName("PreguntaID");
            entity.Property(e => e.RespuestaId).HasColumnName("RespuestaID");

            entity.HasOne(d => d.Intento).WithMany(p => p.RespuestasUsuarios)
                .HasForeignKey(d => d.IntentoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Respuesta__Inten__5629CD9C");

            entity.HasOne(d => d.Pregunta).WithMany(p => p.RespuestasUsuarios)
                .HasForeignKey(d => d.PreguntaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Respuesta__Pregu__571DF1D5");

            entity.HasOne(d => d.Respuesta).WithMany(p => p.RespuestasUsuarios)
                .HasForeignKey(d => d.RespuestaId)
                .HasConstraintName("FK__Respuesta__Respu__5812160E");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RolId).HasName("PK__Roles__F92302D17AD62DCC");

            entity.HasIndex(e => e.Nombre, "UQ__Roles__75E3EFCF53326237").IsUnique();

            entity.Property(e => e.RolId).HasColumnName("RolID");
            entity.Property(e => e.Nombre).HasMaxLength(50);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.UsuarioId).HasName("PK__Usuarios__2B3DE7985EADF0C8");

            entity.HasIndex(e => e.Email, "UQ__Usuarios__A9D105343BC91B6E").IsUnique();

            entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");
            entity.Property(e => e.Contraseña).HasMaxLength(256);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Nombre).HasMaxLength(100);
        });

        modelBuilder.Entity<UsuarioRole>(entity =>
        {
            entity.HasKey(e => e.UsuarioRolId).HasName("PK__UsuarioR__C869CD2AC4ACE366");

            entity.Property(e => e.UsuarioRolId).HasColumnName("UsuarioRolID");
            entity.Property(e => e.RolId).HasColumnName("RolID");
            entity.Property(e => e.UsuarioId).HasColumnName("UsuarioID");

            entity.HasOne(d => d.Rol).WithMany(p => p.UsuarioRoles)
                .HasForeignKey(d => d.RolId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UsuarioRo__RolID__3F466844");

            entity.HasOne(d => d.Usuario).WithMany(p => p.UsuarioRoles)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UsuarioRo__Usuar__3E52440B");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
