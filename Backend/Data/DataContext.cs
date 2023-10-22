using Microsoft.EntityFrameworkCore;
using Backend.Models;

namespace Backend.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<UserTest> UserTest => Set<UserTest>();
        public DbSet<User> Users => Set<User>(); 
        public DbSet<Cart> ShoppingCarts => Set<Cart>();
        public DbSet<PaymentMethod> UserPaymentMethods => Set<PaymentMethod>();
        public DbSet<PaymentInfo> AccountInfos => Set<PaymentInfo>();
        public DbSet<Item> Items => Set<Item>();
        public DbSet<CartItem> UserCartItems => Set<CartItem>();
        public DbSet<Purchase> Purchases => Set<Purchase>();
        public DbSet<ItemReview> ItemReviews => Set<ItemReview>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<ItemCategory> ItemCategories => Set<ItemCategory>();
        public DbSet<Software> Software => Set<Software>();
        public DbSet<SoftwareCategory> SoftwareCategories => Set<SoftwareCategory>();
 
        //Specify relationships
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
                //User Cart To User (one-to-one)
                modelBuilder.Entity<Cart>()
                    .HasOne(u => u.User)
                    .WithOne(s => s.Cart)
                    .HasForeignKey<Cart>(u => u.UserId)
                    .IsRequired();
                
                //User Payment Method To User (many-to-one) 
                modelBuilder.Entity<PaymentMethod>()
                    .HasOne(pm => pm.User)
                    .WithMany(u => u.PaymentMethods)
                    .HasForeignKey(pm => pm.UserId)
                    .IsRequired();
                
                //User Payment Method to Account Into (one-to-one)
                modelBuilder.Entity<PaymentMethod>()
                    .HasOne(pm => pm.PaymentInfo) 
                    .WithOne(pi => pi.PaymentMethod)
                    .HasForeignKey<PaymentMethod>(pm => pm.PaymentInfoId) 
                    .IsRequired();
                
                //User To Package (one-to-many)
                modelBuilder.Entity<Item>()
                    .HasOne(i => i.User)
                    .WithMany(u => u.PublishedPackages)
                    .HasForeignKey(i => i.PublisherId)
                    .IsRequired();
                
                //User Cart To User Cart Item (one-to-many)
                modelBuilder.Entity<Cart>()
                    .HasMany(c => c.CartItems)
                    .WithOne(ci => ci.Cart)
                    .HasForeignKey(c => c.CartId)
                    .IsRequired();
                
                //Item to User Cart Item (one-to-may)
                modelBuilder.Entity<Item>()
                    .HasMany(i => i.CartItems)
                    .WithOne(ci => ci.Item)
                    .HasForeignKey(i => i.ItemId)
                    .IsRequired();
                
                //Item to Purchase (one-to-many)
                modelBuilder.Entity<Item>()
                    .HasMany(i => i.Purchases)
                    .WithOne(p => p.Item)
                    .HasForeignKey(i => i.ItemId)
                    .IsRequired();
                
                //User To Purchase (one-to-many)
                modelBuilder.Entity<User>()
                    .HasMany(u => u.Purchases)
                    .WithOne(p => p.User)
                    .HasForeignKey(u => u.UserId)
                    .IsRequired();
                
                //User To Item Review (one-to-many)
                modelBuilder.Entity<User>()
                    .HasMany(u => u.ItemReviews)
                    .WithOne(ir => ir.User)
                    .HasForeignKey(ir => ir.UserId)
                    .IsRequired();
                
                //Item To Item Review (one-to-many)
                modelBuilder.Entity<Item>()
                    .HasMany(i => i.Reviews)
                    .WithOne(ir => ir.Item)
                    .HasForeignKey(ir => ir.ItemId)
                    .IsRequired();
                
                //Item to Item Category (one-to-many)
                modelBuilder.Entity<Item>()
                    .HasMany(i => i.ItemCategories)
                    .WithOne(ic => ic.Item)
                    .HasForeignKey(ic => ic.ItemId)
                    .IsRequired();
                
                //Category To Item Category(one-to-many)
                modelBuilder.Entity<Category>()
                    .HasMany(c => c.ItemCategories)
                    .WithOne(ic => ic.Category)
                    .HasForeignKey(ic => ic.CategoryId)
                    .IsRequired();
                
                //Category To Software Category(one-to-many)
                modelBuilder.Entity<Category>()
                    .HasMany(c => c.SoftwareCategories)
                    .WithOne(sc => sc.Category)
                    .HasForeignKey(sc => sc.CategoryId)
                    .IsRequired();
                
                //Software To Software Category
                modelBuilder.Entity<Software>()
                    .HasMany(s => s.SoftwareCategories)
                    .WithOne(sc => sc.Software)
                    .HasForeignKey(sc => sc.SoftwareId)
                    .IsRequired();
                    
            base.OnModelCreating(modelBuilder);
        }
    }
}