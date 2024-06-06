using Microsoft.EntityFrameworkCore;
using WebmasterAPI.Authentication.Domain.Models;
using WebmasterAPI.Models;
namespace WebmasterAPI.Shared.Persistence.Contexts
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
        public DbSet<Developer> Developers { get; set; }
        public DbSet<Enterprise> Enterprises { get; set; }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            // User Configuration
            builder.Entity<User>().ToTable("Users");
            builder.Entity<User>().HasKey(u => u.user_id);
            builder.Entity<User>().Property(u => u.user_id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<User>().Property(u => u.mail).IsRequired().HasMaxLength(64);
            builder.Entity<User>().Property(u => u.password).IsRequired().HasMaxLength(32);
            builder.Entity<User>().Property(u => u.user_type).IsRequired();
            
            // Developer Configuration
            builder.Entity<Developer>().ToTable("Developers");
            builder.Entity<Developer>().HasKey(d => d.developer_id);
            builder.Entity<Developer>().Property(d => d.developer_id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Developer>().HasOne(d => d.User)
                .WithMany()
                .HasForeignKey(d => d.user_id);
            builder.Entity<Developer>().Property(d => d.firstName).IsRequired().HasMaxLength(64);
            builder.Entity<Developer>().Property(d => d.lastName).IsRequired().HasMaxLength(64);
            builder.Entity<Developer>().Property(d => d.description).HasColumnType("TEXT");
            builder.Entity<Developer>().Property(d => d.country).HasMaxLength(32);
            builder.Entity<Developer>().Property(d => d.phone).HasMaxLength(16);
            builder.Entity<Developer>().Property(d => d.completed_projects).IsRequired();
            builder.Entity<Developer>().Property(d => d.specialties).HasMaxLength(128);
            builder.Entity<Developer>().Property(d => d.profile_img_url).HasMaxLength(512);
            
            // Enterprise Configuration
            builder.Entity<Enterprise>().ToTable("Enterprises");
            builder.Entity<Enterprise>().HasKey(e => e.enterprise_id);
            builder.Entity<Enterprise>().Property(e => e.enterprise_id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Enterprise>().HasOne(e => e.User)
                .WithMany()
                .HasForeignKey(e => e.user_id);
            builder.Entity<Enterprise>().Property(e => e.enterprise_name).IsRequired().HasMaxLength(64);
            builder.Entity<Enterprise>().Property(e => e.description).HasColumnType("TEXT");
            builder.Entity<Enterprise>().Property(e => e.country).HasMaxLength(32);
            builder.Entity<Enterprise>().Property(e => e.RUC).HasMaxLength(32);
            builder.Entity<Enterprise>().Property(e => e.phone).HasMaxLength(16);
            builder.Entity<Enterprise>().Property(e => e.website).HasMaxLength(64);
            builder.Entity<Enterprise>().Property(e => e.profile_img_url).HasMaxLength(512);
            builder.Entity<Enterprise>().Property(e => e.sector).HasMaxLength(32);
        }
    }
}