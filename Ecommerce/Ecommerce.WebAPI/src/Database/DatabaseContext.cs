using Ecommerce.Core.src.Entities;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace Ecommerce.WebAPI.src.Database
{
    public class DatabaseContext : DbContext
    {
        private readonly IConfiguration _config;
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Review> Reviews { get; set; }
        static DatabaseContext()
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        public DatabaseContext(DbContextOptions options, IConfiguration config) : base(options)
        {
            _config = config;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            /*
            var builder = new NpgsqlDataSourceBuilder(_config.GetConnectionString("ElephantDb"));
            builder.MapEnum<Role>();
            builder.MapEnum<OrderStatus>();
            optionsBuilder.UseNpgsql(builder.Build())
                .UseSnakeCaseNamingConvention()
                   .AddInterceptors(new TimeStampInterceptor())
                   .EnableSensitiveDataLogging()
                   .EnableDetailedErrors();
            */
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresEnum<Role>();
            modelBuilder.HasPostgresEnum<OrderStatus>();
            // modelBuilder.Entity<User>(entity => entity.Property(e => e.Role).HasColumnType("role"));

            modelBuilder.Entity<ProductLine>()
           .HasOne(p => p.Category)
           .WithMany(c => c.ProductLines)
           .HasForeignKey(p => p.CategoryId);


            modelBuilder.Entity<Product>()
           .HasOne(p => p.ProductLine)
           .WithMany(c => c.Products)
           .HasForeignKey(p => p.ProductLineId);


            modelBuilder.Entity<Product>()
           .HasOne(p => p.ProductSize)
           .WithMany(c => c.Products)
           .HasForeignKey(p => p.ProductSizeId);

            modelBuilder.Entity<OrderItem>().HasKey(e => new { e.ProductId, e.OrderId });

            modelBuilder.Entity<Category>(e =>
            {
                e.HasData(SeedingData.GetCategories());
            });

            modelBuilder.Entity<ProductSize>(e =>
            {
                e.HasData(SeedingData.GetProductSizes());
            });

            modelBuilder.Entity<ProductLine>(e =>
            {
                e.HasData(SeedingData.GetProductLines());
            });

            modelBuilder.Entity<Image>(e =>
            {
                e.HasData(SeedingData.GetImages());
            });

            modelBuilder.Entity<Product>(e =>
            {
                e.HasData(SeedingData.GetProducts());
            });


            modelBuilder.Entity<User>(e =>
            {
                e.HasData(SeedingData.GetUser());
            });

            modelBuilder.Entity<Address>(e =>
            {
                e.HasData(SeedingData.GetAddresses());
            });

            modelBuilder.Entity<Avatar>(e =>
            {
                e.HasData(SeedingData.GetAvatars());
            });


        }

    }
}