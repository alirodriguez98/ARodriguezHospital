using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DL;

public partial class ArodriguezHospitalContext : DbContext
{
    public ArodriguezHospitalContext()
    {
    }

    public ArodriguezHospitalContext(DbContextOptions<ArodriguezHospitalContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Especialidad> Especialidads { get; set; }

    public virtual DbSet<Hospital> Hospitals { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.; Database=ARodriguezHospital; Trusted_Connection=True; TrustServerCertificate=True; User ID=sa; Password=pass@word1;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Especialidad>(entity =>
        {
            entity.HasKey(e => e.IdEspecialidad).HasName("PK__Especial__693FA0AFCCBD3146");

            entity.ToTable("Especialidad");

            entity.Property(e => e.Nombre)
                .HasMaxLength(40)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Hospital>(entity =>
        {
            entity.HasKey(e => e.IdHospital).HasName("PK__Hospital__A80B2F2D10505E65");

            entity.ToTable("Hospital");

            entity.Property(e => e.AnioConstruccion).HasColumnType("datetime");
            entity.Property(e => e.Direccion)
                .HasMaxLength(60)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(40)
                .IsUnicode(false);

            entity.HasOne(d => d.IdEspecialidadNavigation).WithMany(p => p.Hospitals)
                .HasForeignKey(d => d.IdEspecialidad)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Hospital__IdEspe__1273C1CD");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
