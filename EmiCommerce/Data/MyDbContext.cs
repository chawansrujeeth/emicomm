using System;
using System.Collections.Generic;
using EmiCommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace EmiCommerce.Data;

public partial class MyDbContext : DbContext
{
    public MyDbContext()
    {
    }

    public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<User> Users { get; set; }


    public virtual DbSet<Product> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Configuration is provided via DI in Program.cs using the connection string from appsettings.
        // Intentionally left empty to avoid hardcoded connection strings that break on other machines.
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("Users");
            entity.HasKey(e => e.UserId);
            entity.Property(e => e.UserId)
                .UseIdentityColumn();
            entity.Property(e => e.Username).HasMaxLength(100).IsRequired();
            entity.Property(e => e.Email).HasMaxLength(100).IsRequired();
            entity.Property(e => e.Phone).HasMaxLength(20).IsRequired();
            entity.Property(e => e.PasswordHash).IsRequired();
            entity.Property(e => e.Role).HasMaxLength(50).IsRequired();
            // Ensure values are provided by the app and included in INSERT
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .IsRequired()
                .ValueGeneratedNever();
            entity.Property(e => e.IsActive)
                .HasColumnType("bit")
                .IsRequired()
                .ValueGeneratedNever();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
