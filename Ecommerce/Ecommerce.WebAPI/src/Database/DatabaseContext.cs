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
            var builder = new NpgsqlDataSourceBuilder(_config.GetConnectionString("LocalDb"));
            builder.MapEnum<Role>();
            builder.MapEnum<OrderStatus>();
            optionsBuilder.UseNpgsql(builder.Build())
                .UseSnakeCaseNamingConvention()
                   .AddInterceptors(new TimeStampInterceptor());
            // optionsBuilder.UseNpgsql("Host=localhost;Database=shopify;Username=admin");
            //base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresEnum<Role>();
            modelBuilder.HasPostgresEnum<OrderStatus>();
           // modelBuilder.Entity<User>(entity => entity.Property(e => e.Role).HasColumnType("role"));

            modelBuilder.Entity<Product>()
           .HasOne(p => p.Category)
           .WithMany(c => c.Products)
           .HasForeignKey(p => p.CategoryId);

            modelBuilder.Entity<OrderItem>().HasKey(e => new { e.ProductId, e.OrderId });
        }

    }
}