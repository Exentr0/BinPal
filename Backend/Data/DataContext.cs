using Microsoft.EntityFrameworkCore;
using Backend.Models;

namespace Backend.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        
        public DbSet<User> Users => Set<User>(); 
        public DbSet<ShoppingCart> ShoppingCarts => Set<ShoppingCart>();
        public DbSet<PaymentMethod> UserPaymentMethods => Set<PaymentMethod>();
        public DbSet<PaymentDetails> AccountInfos => Set<PaymentDetails>();
        public DbSet<Item> Items => Set<Item>();
        public DbSet<CartItem> UserCartItems => Set<CartItem>();
        public DbSet<Purchase> Purchases => Set<Purchase>();
        public DbSet<ItemReview> ItemReviews => Set<ItemReview>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<ItemCategory> ItemCategories => Set<ItemCategory>();
        public DbSet<Software> Software => Set<Software>();
        public DbSet<SoftwareCategory> SoftwareCategories => Set<SoftwareCategory>();
        public DbSet<Plugin> Plugins => Set<Plugin>();
        public DbSet<ItemSoftware> ItemSoftware => Set<ItemSoftware>();
        public DbSet<ItemPlugin> ItemPlugins => Set<ItemPlugin>();
 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
                //User Cart To User (one-to-one)
                modelBuilder.Entity<ShoppingCart>()
                    .HasOne(u => u.User)
                    .WithOne(s => s.ShoppingCart)
                    .HasForeignKey<ShoppingCart>(u => u.UserId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired();
                
                //User Payment Method To User (many-to-one) 
                modelBuilder.Entity<PaymentMethod>()
                    .HasOne(pm => pm.User)
                    .WithMany(u => u.PaymentMethods)
                    .HasForeignKey(pm => pm.UserId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired();
                
                //User Payment Method to Account Into (one-to-one)
                modelBuilder.Entity<PaymentMethod>()
                    .HasOne(pm => pm.PaymentDetails) 
                    .WithOne(pi => pi.PaymentMethod)
                    .HasForeignKey<PaymentMethod>(pm => pm.PaymentDetailsId) 
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired();
                
                //User To Package (one-to-many)
                modelBuilder.Entity<Item>()
                    .HasOne(i => i.User)
                    .WithMany(u => u.PublishedPackages)
                    .HasForeignKey(i => i.PublisherId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired();
            
                modelBuilder.Entity<CartItem>()
                    .HasKey(ci => new { ci.ItemId, ci.CartId });
                
                //User Cart To User Cart Item (one-to-many)
                modelBuilder.Entity<ShoppingCart>()
                    .HasMany(c => c.CartItems)
                    .WithOne(ci => ci.ShoppingCart)
                    .HasForeignKey(c => c.CartId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired();
                
                //Item to User Cart Item (one-to-may)
                modelBuilder.Entity<Item>()
                    .HasMany(i => i.CartItems)
                    .WithOne(ci => ci.Item)
                    .HasForeignKey(i => i.ItemId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired();
                
                //Item to Purchase (one-to-many)
                modelBuilder.Entity<Item>()
                    .HasMany(i => i.Purchases)
                    .WithOne(p => p.Item)
                    .HasForeignKey(i => i.ItemId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired();
                
                //User To Purchase (one-to-many)
                modelBuilder.Entity<User>()
                    .HasMany(u => u.Purchases)
                    .WithOne(p => p.User)
                    .HasForeignKey(u => u.UserId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired();
            
                
                //User To Item Review (one-to-many)
                modelBuilder.Entity<User>()
                    .HasMany(u => u.ItemReviews)
                    .WithOne(ir => ir.User)
                    .HasForeignKey(ir => ir.UserId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired();
                
                //Item To Item Review (one-to-many)
                modelBuilder.Entity<Item>()
                    .HasMany(i => i.Reviews)
                    .WithOne(ir => ir.Item)
                    .HasForeignKey(ir => ir.ItemId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired();
                
                modelBuilder.Entity<ItemCategory>()
                    .HasKey(ic => new { ic.ItemId, ic.CategoryId });
                
                //Item to Item Category (one-to-many)
                modelBuilder.Entity<Item>()
                    .HasMany(i => i.ItemCategories)
                    .WithOne(ic => ic.Item)
                    .HasForeignKey(ic => ic.ItemId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired();
                
                //Category To Item Category(one-to-many)
                modelBuilder.Entity<Category>()
                    .HasMany(c => c.ItemCategories)
                    .WithOne(ic => ic.Category)
                    .HasForeignKey(ic => ic.CategoryId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired();
                
                modelBuilder.Entity<SoftwareCategory>()
                    .HasKey(sc => new { sc.SoftwareId, sc.CategoryId });
                
                //Category To Software Category(one-to-many)
                modelBuilder.Entity<Category>()
                    .HasMany(c => c.SoftwareCategories)
                    .WithOne(sc => sc.Category)
                    .HasForeignKey(sc => sc.CategoryId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired();
                
                //Software To Software Category
                modelBuilder.Entity<Software>()
                    .HasMany(s => s.SoftwareCategories)
                    .WithOne(sc => sc.Software)
                    .HasForeignKey(sc => sc.SoftwareId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired();
                
                
                modelBuilder.Entity<ItemSoftware>()
                    .HasKey(si => new { si.ItemId, si.SoftwareId });
                
                                
                modelBuilder.Entity<Item>()
                    .Property(i => i.Price)
                    .HasColumnType("decimal(18,2)");

                //Item to ItemSoftware (one-to-many)
                modelBuilder.Entity<Item>()
                    .HasMany(i => i.ItemSoftware)
                    .WithOne(its => its.Item)
                    .HasForeignKey(its => its.ItemId).OnDelete(DeleteBehavior.Restrict)
                    .IsRequired();

                //Software to ItemSoftware(one-to-many)
                modelBuilder.Entity<Software>()
                    .HasMany(s => s.SoftwareItem)
                    .WithOne(its => its.Software)
                    .HasForeignKey(its => its.SoftwareId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired();
            
                
                
                modelBuilder.Entity<ItemPlugin>()
                    .HasKey(ip => new { ip.ItemId, ip.PluginId });

            
                //Plugin To Item Plugin(one-to-many)
                modelBuilder.Entity<Plugin>()
                    .HasMany(p => p.ItemPlugins)
                    .WithOne(ip => ip.Plugin)
                    .HasForeignKey(ip => ip.PluginId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired();
                
                //Item To Item Plugin(one-to-many)
                modelBuilder.Entity<Item>()
                    .HasMany(i => i.ItemPlugins)
                    .WithOne(ip => ip.Item)
                    .HasForeignKey(ip => ip.ItemId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired();
                 
            base.OnModelCreating(modelBuilder);
        }
    }
}