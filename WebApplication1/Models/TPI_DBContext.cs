using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace WebApplication1.Models
{
    public partial class TPI_DBContext : DbContext
    {
        public TPI_DBContext()
        {
        }

        public TPI_DBContext(DbContextOptions<TPI_DBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Categorium> Categoria { get; set; }
        public virtual DbSet<Comentario> Comentarios { get; set; }
        public virtual DbSet<Cursadum> Cursada { get; set; }
        public virtual DbSet<Entrega> Entregas { get; set; }
        public virtual DbSet<Equipo> Equipos { get; set; }
        public virtual DbSet<EquipoPersona> EquipoPersonas { get; set; }
        public virtual DbSet<Persona> Personas { get; set; }
        public virtual DbSet<Proyecto> Proyectos { get; set; }
        public virtual DbSet<Red> Reds { get; set; }
        public virtual DbSet<TipoPersona> TipoPersonas { get; set; }
        public virtual DbSet<TipoProyect> TipoProyects { get; set; }
        public virtual DbSet<TipoRed> TipoReds { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");

            modelBuilder.Entity<Categorium>(entity =>
            {
                entity.HasKey(e => e.IdCategoria);

                entity.ToTable("categoria");

                entity.Property(e => e.IdCategoria).HasColumnName("idCategoria");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("descripcion")
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<Comentario>(entity =>
            {
                entity.HasKey(e => e.IdComentario);

                entity.ToTable("comentario");

                entity.Property(e => e.IdComentario).HasColumnName("idComentario");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("descripcion")
                    .IsFixedLength(true);

                entity.Property(e => e.Fecha)
                    .HasColumnType("date")
                    .HasColumnName("fecha");

                entity.Property(e => e.IdPersona).HasColumnName("idPersona");

                entity.Property(e => e.IdProyecto).HasColumnName("idProyecto");

                entity.HasOne(d => d.IdPersonaNavigation)
                    .WithMany(p => p.Comentarios)
                    .HasForeignKey(d => d.IdPersona)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_persona_id");

                entity.HasOne(d => d.IdProyectoNavigation)
                    .WithMany(p => p.Comentarios)
                    .HasForeignKey(d => d.IdProyecto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_proyect_idProyecto");
            });

            modelBuilder.Entity<Cursadum>(entity =>
            {
                entity.HasKey(e => e.IdCursada);

                entity.ToTable("cursada");

                entity.Property(e => e.IdCursada).HasColumnName("idCursada");

                entity.Property(e => e.CodCursada)
                    .HasMaxLength(30)
                    .HasColumnName("codCursada")
                    .IsFixedLength(true);

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(30)
                    .HasColumnName("descripcion")
                    .IsFixedLength(true);

                entity.Property(e => e.FechaFin)
                    .HasColumnType("date")
                    .HasColumnName("fechaFin");

                entity.Property(e => e.FechaInicio)
                    .HasColumnType("date")
                    .HasColumnName("fechaInicio");
            });

            modelBuilder.Entity<Entrega>(entity =>
            {
                entity.HasKey(e => e.IdEntrega);

                entity.ToTable("entrega");

                entity.Property(e => e.IdEntrega).HasColumnName("idEntrega");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("descripcion")
                    .IsFixedLength(true);

                entity.Property(e => e.IdProyecto).HasColumnName("idProyecto");

                entity.HasOne(d => d.IdProyectoNavigation)
                    .WithMany(p => p.Entregas)
                    .HasForeignKey(d => d.IdProyecto)
                    .HasConstraintName("FK_entrega_idProyecto");
            });

            modelBuilder.Entity<Equipo>(entity =>
            {
                entity.HasKey(e => e.IdEquipo);

                entity.ToTable("equipo");

                entity.Property(e => e.IdEquipo).HasColumnName("idEquipo");

                entity.Property(e => e.IdProyecto).HasColumnName("idProyecto");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("nombre")
                    .IsFixedLength(true);

                entity.HasOne(d => d.IdProyectoNavigation)
                    .WithMany(p => p.Equipos)
                    .HasForeignKey(d => d.IdProyecto)
                    .HasConstraintName("FK_proyecto_id");
            });

            modelBuilder.Entity<EquipoPersona>(entity =>
            {
                entity.HasKey(e => new { e.IdEquipo, e.IdPersona });

                entity.ToTable("equipoPersona");

                entity.Property(e => e.IdEquipo).HasColumnName("idEquipo");

                entity.Property(e => e.IdPersona).HasColumnName("idPersona");

                entity.HasOne(d => d.IdEquipoNavigation)
                    .WithMany(p => p.EquipoPersonas)
                    .HasForeignKey(d => d.IdEquipo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_equipo_idEquipo");

                entity.HasOne(d => d.IdPersonaNavigation)
                    .WithMany(p => p.EquipoPersonas)
                    .HasForeignKey(d => d.IdPersona)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_person_id");
            });

            modelBuilder.Entity<Persona>(entity =>
            {
                entity.ToTable("persona");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Avatar)
                    .HasMaxLength(50)
                    .HasColumnName("avatar")
                    .IsFixedLength(true);

                entity.Property(e => e.Carrera)
                    .HasMaxLength(50)
                    .HasColumnName("carrera")
                    .IsFixedLength(true);

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");

                entity.Property(e => e.Dni).HasColumnName("DNI");

                entity.Property(e => e.Edad).HasColumnName("edad");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsFixedLength(true);

                entity.Property(e => e.EmailUnlam)
                    .HasMaxLength(50)
                    .IsFixedLength(true);

                entity.Property(e => e.IdCursada).HasColumnName("idCursada");

                entity.Property(e => e.IdTipo).HasColumnName("idTipo");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("nombre")
                    .IsFixedLength(true);

                entity.Property(e => e.Password)
                    .HasMaxLength(10)
                    .HasColumnName("password")
                    .IsFixedLength(true);

                entity.HasOne(d => d.IdCursadaNavigation)
                    .WithMany(p => p.Personas)
                    .HasForeignKey(d => d.IdCursada)
                    .HasConstraintName("FK_cursada_idCursada");

                entity.HasOne(d => d.IdTipoNavigation)
                    .WithMany(p => p.Personas)
                    .HasForeignKey(d => d.IdTipo)
                    .HasConstraintName("FK_persona_tipoPersona");
            });

            modelBuilder.Entity<Proyecto>(entity =>
            {
                entity.HasKey(e => e.IdProyecto);

                entity.ToTable("proyecto");

                entity.Property(e => e.IdProyecto).HasColumnName("idProyecto");

                entity.Property(e => e.CantMeGusta).HasColumnName("cantMeGusta");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");

                entity.Property(e => e.IdCategoria).HasColumnName("idCategoria");

                entity.Property(e => e.IdRed).HasColumnName("idRed");

                entity.Property(e => e.IdTipoProyecto).HasColumnName("idTipoProyecto");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasColumnName("nombre")
                    .IsFixedLength(true);

                entity.Property(e => e.Repositorio)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("repositorio");

                entity.Property(e => e.RutaFoto)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("rutaFoto");

                entity.Property(e => e.RutaVideo)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("rutaVideo");

                entity.HasOne(d => d.IdCategoriaNavigation)
                    .WithMany(p => p.Proyectos)
                    .HasForeignKey(d => d.IdCategoria)
                    .HasConstraintName("FK_categoria_idCategoria");

                entity.HasOne(d => d.IdTipoProyectoNavigation)
                    .WithMany(p => p.Proyectos)
                    .HasForeignKey(d => d.IdTipoProyecto)
                    .HasConstraintName("FK_proyecto_idTipoProyecto");
            });

            modelBuilder.Entity<Red>(entity =>
            {
                entity.HasKey(e => e.IdRed)
                    .HasName("PK_redes");

                entity.ToTable("red");

                entity.Property(e => e.IdRed).HasColumnName("idRed");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("descripcion")
                    .IsFixedLength(true);

                entity.Property(e => e.IdProyecto).HasColumnName("idProyecto");

                entity.HasOne(d => d.IdProyectoNavigation)
                    .WithMany(p => p.Reds)
                    .HasForeignKey(d => d.IdProyecto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_proyecto_idProyecto");
            });

            modelBuilder.Entity<TipoPersona>(entity =>
            {
                entity.HasKey(e => e.IdTipo);

                entity.ToTable("tipoPersona");

                entity.Property(e => e.IdTipo)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("idTipo");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(20)
                    .HasColumnName("descripcion")
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<TipoProyect>(entity =>
            {
                entity.HasKey(e => e.IdTipoProyecto);

                entity.ToTable("tipoProyect");

                entity.Property(e => e.IdTipoProyecto).HasColumnName("idTipoProyecto");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("descripcion")
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<TipoRed>(entity =>
            {
                entity.HasKey(e => e.IdTipoRed);

                entity.ToTable("tipoRed");

                entity.Property(e => e.IdTipoRed).HasColumnName("idTipoRed");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("descripcion")
                    .IsFixedLength(true);

                entity.Property(e => e.IdRed).HasColumnName("idRed");

                entity.HasOne(d => d.IdRedNavigation)
                    .WithMany(p => p.TipoReds)
                    .HasForeignKey(d => d.IdRed)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_red_idRed");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
