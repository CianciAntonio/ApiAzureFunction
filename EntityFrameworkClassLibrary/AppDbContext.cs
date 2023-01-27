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

        #region ONCONFIGURING METHOD (ENABLE when add migration)
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=EsercizioAPI;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        //    }
        //}
        #endregion

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


            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}