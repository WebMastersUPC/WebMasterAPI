using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WebmasterAPI.Models;

namespace WebmasterAPI.Data
{
    public class AppDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public AppDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(_configuration.GetConnectionString("DefaultConnection"), ServerVersion.AutoDetect(_configuration.GetConnectionString("DefaultConnection")));
        }
        public DbSet<User> Users { get; set; } 
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            // Account Configuration
            builder.Entity<User>().ToTable("Users");
            builder.Entity<User>().HasKey(u => u.user_id);
            builder.Entity<User>().Property(u => u.user_id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<User>().Property(u => u.mail).IsRequired().HasMaxLength(64);
            builder.Entity<User>().Property(u => u.password).IsRequired().HasMaxLength(32);
            builder.Entity<User>().Property(u => u.names).IsRequired().HasMaxLength(64);
            builder.Entity<User>().Property(u => u.lastnames).IsRequired().HasMaxLength(64);
            builder.Entity<User>().Property(u => u.cellphone).IsRequired().HasMaxLength(16);
            builder.Entity<User>().Property(u => u.user_type).IsRequired();
            builder.Entity<User>().Property(u => u.profile_img_url).IsRequired().HasMaxLength(256);

        }
    }
}