using Microsoft.EntityFrameworkCore;
using EntityFrameworkClassLibrary.Models;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Extensions.Configuration;
using System.ComponentModel;

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

        //Meteodo attraverso il quale definisco connection string per connessione al database
        //(Da capire perchè non la prende con GetEnvironementVariable, ma devo passarla esplicita per far funzionare migrazione)

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var kvaultUrl = Environment.GetEnvironmentVariable("VaultUri");
            var secretClient = new SecretClient(new Uri(kvaultUrl), new DefaultAzureCredential());
            var secret = secretClient.GetSecret("AzureConnectionString-AC").Value.Value;

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(secret);
            }
        }

        //Creazione del modello di relazione tra le tabelle del database
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