using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PruebaAppApi.DataAccess.Entities;

public partial class ValentechCoreContext : DbContext
{
    public ValentechCoreContext()
    {
    }

    public ValentechCoreContext(DbContextOptions<ValentechCoreContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Patient> Patient { get; set; }

    public virtual DbSet<User> User { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionString");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Patient>(entity =>
        {
            entity.Property(e => e.ID).ValueGeneratedNever();
            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.Disease).IsUnicode(false);
            entity.Property(e => e.DocumentNumber).HasMaxLength(15);
            entity.Property(e => e.Email).IsUnicode(false);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.ModificationDate).HasColumnType("datetime");
            entity.Property(e => e.Phone).HasMaxLength(10);

            entity.HasOne(d => d.CreationUserNavigation).WithMany(p => p.PatientCreationUserNavigation)
                .HasForeignKey(d => d.CreationUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Patient_User");

            entity.HasOne(d => d.ModificationUserNavigation).WithMany(p => p.PatientModificationUserNavigation)
                .HasForeignKey(d => d.ModificationUser)
                .HasConstraintName("FK_Patient_User1");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.ID).ValueGeneratedNever();
            entity.Property(e => e.DocumentNumber)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Password).IsUnicode(false);
            entity.Property(e => e.Salt).IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
