using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebAppTestEmployees.Models;

public partial class DipendentiAziendaContext : DbContext
{
    public DipendentiAziendaContext()
    {
    }

    public DipendentiAziendaContext(DbContextOptions<DipendentiAziendaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AnagraficaGenerica> AnagraficaGenericas { get; set; }

    public virtual DbSet<AttivitaDipendente> AttivitaDipendentes { get; set; }

    public virtual DbSet<ErrorLog> ErrorLogs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=DipendentiAzienda;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AnagraficaGenerica>(entity =>
        {
            entity.HasKey(e => e.Matricola);

            entity.ToTable("AnagraficaGenerica");

            entity.Property(e => e.Matricola).HasMaxLength(5);
            entity.Property(e => e.Cap)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Citta).HasMaxLength(50);
            entity.Property(e => e.Indirizzo).HasMaxLength(100);
            entity.Property(e => e.Nominativo).HasMaxLength(50);
            entity.Property(e => e.Provincia).HasMaxLength(50);
            entity.Property(e => e.Reparto).HasMaxLength(50);
            entity.Property(e => e.Ruolo).HasMaxLength(50);
            entity.Property(e => e.Telefono)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<AttivitaDipendente>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.ToTable("AttivitaDipendente");

            entity.Property(e => e.Attivita)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.DataAttivita).HasColumnType("datetime");
            entity.Property(e => e.Matricola).HasMaxLength(5);

            entity.HasOne(d => d.MatricolaNavigation).WithMany(p => p.AttivitaDipendentes)
                .HasForeignKey(d => d.Matricola)
                .HasConstraintName("FK_AttivitaDipendente_AttivitaDipendente");
        });

        modelBuilder.Entity<ErrorLog>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("ErrorLog");

            entity.Property(e => e.Data).HasColumnType("date");
            entity.Property(e => e.Message).HasMaxLength(255);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
