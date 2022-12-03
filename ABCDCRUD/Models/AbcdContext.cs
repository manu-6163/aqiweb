using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ABCDCRUD.Models;

public partial class AbcdContext : DbContext
{
    public AbcdContext()
    {
    }

    public AbcdContext(DbContextOptions<AbcdContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Asegurado> Asegurados { get; set; }

 //   protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
 //       => optionsBuilder.UseSqlServer("server=LAPTOP-5BBASDOI\\SQLEXPRESS; database=ABCD; integrated security=true; Encrypt=false;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Asegurado>(entity =>
        {
            entity.HasKey(e => e.Identificacion).HasName("PK__Asegurad__C196DEC69A8A70A5");

            entity.Property(e => e.Identificacion)
                .ValueGeneratedNever()
                .HasColumnName("identificacion");
            entity.Property(e => e.Apellido1)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("apellido1");
            entity.Property(e => e.Apellido2)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("apellido2");
            entity.Property(e => e.Celular)
                .HasMaxLength(12)
                .IsUnicode(false)
                .HasColumnName("celular");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.FechaNacimiento)
                .HasColumnType("date")
                .HasColumnName("fecha_nacimiento");
            entity.Property(e => e.Nombre1)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre1");
            entity.Property(e => e.Nombre2)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre2");
            entity.Property(e => e.ValorSeguro)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("valor_seguro");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
