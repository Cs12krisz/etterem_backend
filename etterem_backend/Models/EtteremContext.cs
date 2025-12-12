using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace etterem_backend.Models;

public partial class EtteremContext : DbContext
{
    public EtteremContext()
    {
    }

    public EtteremContext(DbContextOptions<EtteremContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Rendele> Rendeles { get; set; }

    public virtual DbSet<Rendelestetel> Rendelestetels { get; set; }

    public virtual DbSet<Termek> Termeks { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("server=localhost;database=etterem;user=root;password=");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Rendele>(entity =>
        {
            entity.HasKey(e => e.RendelesId).HasName("PRIMARY");

            entity.ToTable("rendeles");

            entity.Property(e => e.RendelesId)
                .HasColumnType("int(11)")
                .HasColumnName("RendelesID");
            entity.Property(e => e.AsztalSzam).HasColumnType("int(11)");
            entity.Property(e => e.FizetesMod).HasMaxLength(50);
        });

        modelBuilder.Entity<Rendelestetel>(entity =>
        {
            entity.HasKey(e => e.TetelId).HasName("PRIMARY");

            entity.ToTable("rendelestetel");

            entity.HasIndex(e => e.RendelesId, "RendelesID");

            entity.HasIndex(e => e.TermekId, "TermekID");

            entity.Property(e => e.TetelId)
                .HasColumnType("int(11)")
                .HasColumnName("TetelID");
            entity.Property(e => e.RendelesId)
                .HasColumnType("int(11)")
                .HasColumnName("RendelesID");
            entity.Property(e => e.TermekId)
                .HasColumnType("int(11)")
                .HasColumnName("TermekID");

            entity.HasOne(d => d.Rendeles).WithMany(p => p.Rendelestetels)
                .HasForeignKey(d => d.RendelesId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("rendelestetel_ibfk_1");

            entity.HasOne(d => d.Termek).WithMany(p => p.Rendelestetels)
                .HasForeignKey(d => d.TermekId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("rendelestetel_ibfk_2");
        });

        modelBuilder.Entity<Termek>(entity =>
        {
            entity.HasKey(e => e.TermekId).HasName("PRIMARY");

            entity.ToTable("termek");

            entity.Property(e => e.TermekId)
                .HasColumnType("int(11)")
                .HasColumnName("TermekID");
            entity.Property(e => e.Ar).HasColumnType("int(11)");
            entity.Property(e => e.TermekNev).HasMaxLength(255);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
