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
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC07F5AC0F8E");
            entity.HasIndex(e => e.Email, "UQ__Users__A9D105340AD38BCF").IsUnique();
            entity.Property(e => e.Id)
                .HasColumnName("UserId")
                .ValueGeneratedOnAdd();
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Role).HasMaxLength(50);
            entity.Property(e => e.UserName).HasMaxLength(100);
            entity.Property(e => e.Phone).HasMaxLength(20);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
