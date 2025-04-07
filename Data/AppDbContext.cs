using imsapi.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace imsapi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Store> Stores { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<PaymentItem> PaymentItems { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        modelBuilder.Entity<Store>(entity =>
        {
            entity.HasKey(e => e.id);
            entity.Property(e => e.name).IsRequired().HasMaxLength(100);
            entity.HasMany(e => e.Users)
                .WithOne(e => e.Store)
                .HasForeignKey(e => e.storeId);
            entity.HasMany(e => e.Products)
                .WithOne(e => e.Store)
                .HasForeignKey(e => e.storeId);
            entity.HasMany(e => e.Categories)
                .WithOne(e => e.Store)
                .HasForeignKey(e => e.storeId);
            entity.HasMany(e => e.Payments)
                .WithOne(e => e.Store)
                .HasForeignKey(e => e.storeId);
            entity.HasMany(e => e.Customers)
                .WithOne(e => e.Store)
                .HasForeignKey(e => e.storeId);
        });

        modelBuilder.Entity<Admin>(entity =>
        {
            entity.HasKey(e => e.id);
            entity.Property(e => e.Phone).IsRequired().HasMaxLength(50);
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.id);
            entity.Property(e => e.name).IsRequired().HasMaxLength(50);
            entity.Property(e => e.description).IsRequired().HasMaxLength(255);
            entity.HasOne(e => e.Store)
                .WithMany(e => e.Categories)
                .HasForeignKey(e => e.storeId);
            entity.HasMany(e => e.Products)
                .WithOne(e => e.Category)
                .HasForeignKey(e => e.categoryId);

        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.id);
            entity.Property(e => e.phone).IsRequired().HasMaxLength(100);
            entity.HasOne(e=>e.Store).WithMany(e=>e.Customers).HasForeignKey(e=>e.storeId);
            entity.HasOne(e=>e.User).WithMany(e=>e.Customers).HasForeignKey(e=>e.userId);
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.id);
            entity.Property(e => e.customerId).IsRequired();
            entity.Property(e => e.amount).IsRequired();
            entity.Property(e => e.paymentMethod).IsRequired().HasMaxLength(50);
            entity.HasOne(e => e.Store)
                .WithMany(e => e.Payments)
                .HasForeignKey(e => e.storeId);
            entity.HasOne(e => e.User)
                .WithMany(e => e.Payments)
                .HasForeignKey(e => e.userId);
            entity.HasOne(e => e.Customer)
                .WithMany(e => e.Payments)
                .HasForeignKey(e => e.customerId);
            entity.HasMany(e => e.PaymentItems)
                .WithOne(e => e.Payment)
                .HasForeignKey(e => e.paymentId);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.id);
            entity.Property(e => e.name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.salePrice).IsRequired();
            entity.Property(e => e.purchasePrice).IsRequired();
            entity.HasOne(e=>e.Store)
                .WithMany(e => e.Products)
                .HasForeignKey(e => e.storeId);

            entity.HasOne(e => e.User)
                .WithMany(e => e.Products)
                .HasForeignKey(e => e.userId);

            entity.HasOne(e => e.Category)
                .WithMany(e => e.Products)
                .HasForeignKey(e => e.categoryId);

            entity.HasMany(e => e.PaymentItems)
                .WithOne(e => e.Product)
                .HasForeignKey(e => e.productId);

        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.id);
            entity.Property(e => e.phone).IsRequired().HasMaxLength(100);
            entity.Property(e => e.passwordHash).IsRequired().HasMaxLength(64);
            entity.Property(e => e.fullName).IsRequired().HasMaxLength(100);
            entity.HasOne(e => e.Store)
                .WithMany(e => e.Users)
                .HasForeignKey(e => e.storeId);
            entity.HasMany(e => e.Products)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.userId);
            entity.HasMany(e => e.Customers)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.userId);
            entity.HasMany(e => e.Payments)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.userId);
        });
        }
        public override int SaveChanges()
        {

            return base.SaveChanges();
        }
    }
}