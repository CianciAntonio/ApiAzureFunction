using Microsoft.EntityFrameworkCore;
using EntityFrameworkClassLibrary.Models;

namespace EntityFrameworkClassLibrary
{
    public partial class AppDbContext : DbContext
    {
        //Costruttore vuoto necessario poiché quando lancio migrazione, trovandomi in una class library esterna alla azure function,
        //non ho un oggetto di configurazione da passare
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        //Costruzione delle Tabelle del Database
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Invoice> Invoices { get; set; }
        public virtual DbSet<Product> Product { get; set; }

        //Creazione del modello di relazione tra le tabelle del database
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer");
                entity.Property(e => e.LastName).HasMaxLength(50);
                entity.Property(e => e.SurName).HasMaxLength(50);
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

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            AddTimeStamps();
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void AddTimeStamps()
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is Timestamp && (
                        e.State == EntityState.Added
                        || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((Timestamp)entityEntry.Entity).UpdatedDate = DateTime.Now;

                if (entityEntry.State == EntityState.Added)
                {
                    ((Timestamp)entityEntry.Entity).CreatedDate = DateTime.Now;
                }
            }
        }
    }
}