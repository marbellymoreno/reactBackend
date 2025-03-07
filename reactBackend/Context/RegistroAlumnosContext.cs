using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using reactBackend.Models;

namespace reactBackend.Context;

public partial class RegistroAlumnosContext : DbContext
{
    public RegistroAlumnosContext()
    {
    }

    public RegistroAlumnosContext(DbContextOptions<RegistroAlumnosContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Alumno> Alumnos { get; set; }

    public virtual DbSet<Asignatura> Asignaturas { get; set; }

    public virtual DbSet<Calificacion> Calificacions { get; set; }

    public virtual DbSet<Matricula> Matriculas { get; set; }

    public virtual DbSet<Profesor> Profesors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=MARBELLYMORA987\\SQLEXPRESS;Initial Catalog=RegistroAlumnos;Integrated Security=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Alumno>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Alumno__3214EC27CF08F450");

            entity.ToTable("Alumno");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Direccion)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Dni)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasColumnName("DNI");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Asignatura>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Asignatu__3214EC27889DB349");

            entity.ToTable("Asignatura");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Profesor)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.ProfesorNavigation).WithMany(p => p.Asignaturas)
                .HasForeignKey(d => d.Profesor)
                .HasConstraintName("FK__Asignatur__Profe__15502E78");
        });

        modelBuilder.Entity<Calificacion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Califica__3214EC270DA6791B");

            entity.ToTable("Calificacion");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.MatriculaId).HasColumnName("MatriculaID");

            entity.HasOne(d => d.Matricula).WithMany(p => p.Calificacions)
                .HasForeignKey(d => d.MatriculaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Calificac__Matri__1BFD2C07");
        });

        modelBuilder.Entity<Matricula>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Matricul__3214EC27A81A3D05");

            entity.ToTable("Matricula");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AlumnoId).HasColumnName("AlumnoID");

            entity.HasOne(d => d.Alumno).WithMany(p => p.Matriculas)
                .HasForeignKey(d => d.AlumnoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Matricula__Alumn__182C9B23");

            entity.HasOne(d => d.AsignaturaNavigation).WithMany(p => p.Matriculas)
                .HasForeignKey(d => d.Asignatura)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Matricula__Asign__1920BF5C");
        });

        modelBuilder.Entity<Profesor>(entity =>
        {
            entity.HasKey(e => e.Usuario).HasName("PK__Profesor__E3237CF6FBCED7A5");

            entity.ToTable("Profesor");

            entity.Property(e => e.Usuario)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(8)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
