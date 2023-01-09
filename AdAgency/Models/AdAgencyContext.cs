using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AdAgency.Models
{
    public partial class AdAgencyContext : DbContext
    {
        public AdAgencyContext()
        {
        }

        public AdAgencyContext(DbContextOptions<AdAgencyContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AdPlace> AdPlaces { get; set; } = null!;
        public virtual DbSet<AdType> AdTypes { get; set; } = null!;
        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<OptionalService> OptionalServices { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<OrdersOptional> OrdersOptionals { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=.\\SQLEXPRESS;Initial Catalog=AdAgency;Integrated Security=True; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AdPlace>(entity =>
            {
                entity.Property(e => e.Cost).HasColumnType("money");

                entity.Property(e => e.Description).HasMaxLength(100);

                entity.Property(e => e.Place).HasMaxLength(50);

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.AdPlaces)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK_AdPlaces_AdTypes");
            });

            modelBuilder.Entity<AdType>(entity =>
            {
                entity.Property(e => e.Description).HasMaxLength(100);

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.Address).HasMaxLength(50);

                entity.Property(e => e.FirstName).HasMaxLength(25);

                entity.Property(e => e.LastName).HasMaxLength(25);

                entity.Property(e => e.MiddleName).HasMaxLength(25);

                entity.Property(e => e.PhoneNumber).HasMaxLength(16);
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.Property(e => e.FirstName).HasMaxLength(25);

                entity.Property(e => e.LastName).HasMaxLength(25);

                entity.Property(e => e.MiddleName).HasMaxLength(25);

                entity.Property(e => e.PhoneNumber).HasMaxLength(16);
            });

            modelBuilder.Entity<OptionalService>(entity =>
            {
                entity.Property(e => e.Cost).HasColumnType("money");

                entity.Property(e => e.Description).HasMaxLength(100);

                entity.Property(e => e.Name).HasMaxLength(20);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.OrderDate).HasColumnType("date");

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_Orders_Customers");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK_Orders_Employees");

                entity.HasOne(d => d.Place)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.PlaceId)
                    .HasConstraintName("FK_Orders_AdPlaces");
            });

            modelBuilder.Entity<OrdersOptional>(entity =>
            {
                entity.HasOne(d => d.Option)
                    .WithMany(p => p.OrdersOptionals)
                    .HasForeignKey(d => d.OptionId)
                    .HasConstraintName("FK_OrdersOptionals_OptionalServices");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrdersOptionals)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_OrdersOptionals_Orders");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
