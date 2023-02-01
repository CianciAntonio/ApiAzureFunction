using Microsoft.EntityFrameworkCore;
using EntityFrameworkClassLibrary.Models;

namespace EntityFrameworkClassLibrary
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Invoice> Invoices { get; set; }
        public virtual DbSet<Product> Product { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string connectionString = Environment.GetEnvironmentVariable("ConnectionString");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer");
                entity.Property(e => e.LastName).HasMaxLength(50);
                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.Invoices)
                .WithOne(e => e.Customer)
                .HasForeignKey(n => n.CustomerId);

            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.ToTable("Invoice");
                entity.Property(e => e.OrderDate).HasMaxLength(11);
                entity.Property(e => e.Quantity).HasColumnType("int");
                entity.Property(e => e.Price).HasColumnType("decimal");
                entity.Property(e => e.Description).HasMaxLength(50);
                entity.Property(e => e.CustomerId).HasColumnType("int");
            });

            modelBuilder.Entity<Invoice>()
                .HasOne(e => e.Product)
                .WithOne(e => e.Invoice)
                .HasForeignKey<Product>(n => n.InvoiceId);

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");
                entity.Property(e => e.ProductName).HasMaxLength(50);
                entity.Property(e => e.ProductDescription).HasMaxLength(50);
                entity.Property(e => e.ProductCategory).HasMaxLength(50);
                entity.Property(e => e.InvoiceId).HasColumnType("int");
            });

            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}