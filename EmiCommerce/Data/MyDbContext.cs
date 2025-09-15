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

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<CartItem> CartItems { get; set; }

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

        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("Products");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id)
                .UseIdentityColumn();
            entity.Property(e => e.Name).HasMaxLength(200).IsRequired();
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.Price).HasColumnType("decimal(18,2)").IsRequired();
            entity.Property(e => e.Stock).IsRequired();
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .IsRequired()
                .ValueGeneratedNever();
        });

        modelBuilder.Entity<Cart>(entity =>
        {
            entity.ToTable("Carts");
            entity.HasKey(e => e.CartId);
            entity.Property(e => e.CartId)
                .UseIdentityColumn();
            entity.Property(e => e.UserId).IsRequired();
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .IsRequired()
                .ValueGeneratedNever();
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .IsRequired()
                .ValueGeneratedNever();
            
            // Foreign key relationship
            entity.HasOne(d => d.User)
                .WithMany()
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<CartItem>(entity =>
        {
            entity.ToTable("CartItems");
            entity.HasKey(e => e.CartItemId);
            entity.Property(e => e.CartItemId)
                .UseIdentityColumn();
            entity.Property(e => e.CartId).IsRequired();
            entity.Property(e => e.ProductId).IsRequired();
            entity.Property(e => e.Quantity).IsRequired();
            entity.Property(e => e.UnitPrice).HasColumnType("decimal(18,2)").IsRequired();
            entity.Property(e => e.AddedAt)
                .HasColumnType("datetime")
                .IsRequired()
                .ValueGeneratedNever();
            
            // Foreign key relationships
            entity.HasOne(d => d.Cart)
                .WithMany(p => p.CartItems)
                .HasForeignKey(d => d.CartId)
                .OnDelete(DeleteBehavior.Cascade);
                
            entity.HasOne(d => d.Product)
                .WithMany()
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
