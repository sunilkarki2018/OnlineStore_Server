using Ecommerce.Core.src.Entities;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace Ecommerce.WebAPI.src.Database
{
    public class DatabaseContext : DbContext
    {
        private readonly IConfiguration _config;
        public DbSet<User> Users { get; set; }
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
            var dataSourceBuilder = new NpgsqlDataSourceBuilder(_config.GetConnectionString("LocalDb"));
            dataSourceBuilder.MapEnum<Role>();
            var dataSource = dataSourceBuilder.Build();
            optionsBuilder
                .UseNpgsql(dataSource)
                .UseSnakeCaseNamingConvention()
                   .AddInterceptors(new TimeStampInterceptor());
            // optionsBuilder.UseNpgsql("Host=localhost;Database=shopify;Username=admin");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresEnum<Role>();
            modelBuilder.Entity<User>(entity => entity.Property(e => e.Role).HasColumnType("role"));

            //modelBuilder.Entity<EventUser>().HasKey(e => new { e.UserId, e.EventId });

        }

    }
}