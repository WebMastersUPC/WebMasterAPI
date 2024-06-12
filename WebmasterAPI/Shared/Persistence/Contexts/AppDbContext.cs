using Microsoft.EntityFrameworkCore;
using WebmasterAPI.Authentication.Domain.Models;
using WebmasterAPI.Messaging.Domain.Models;
using WebmasterAPI.Models;
using WebmasterAPI.ProjectManagement.Domain.Models;

using WebmasterAPI.Support.Domain.Models;
using Developer = WebmasterAPI.Authentication.Domain.Models.Developer;
using Enterprise = WebmasterAPI.Authentication.Domain.Models.Enterprise;
using User = WebmasterAPI.Models.User;
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
        
        public DbSet<Deliverable> Deliverables { get; set; }
        public DbSet<Project> Projects { get; set; }
        
        public DbSet<SupportRequest> SupportRequests { get; set; }
        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            // User Configuration
            builder.Entity<User>().ToTable("Users");
            builder.Entity<User>().HasKey(u => u.user_id);
            builder.Entity<User>().Property(u => u.user_id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<User>().Property(u => u.mail).IsRequired().HasMaxLength(64);
            builder.Entity<User>().Property(u => u.passwordHashed).IsRequired();
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
            
            // Deliverable Configuration
            builder.Entity<Deliverable>().ToTable("Deliverables");
            builder.Entity<Deliverable>().HasKey(d => d.deliverable_id);
            builder.Entity<Deliverable>().Property(d => d.deliverable_id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Deliverable>().Property(d=> d.title).IsRequired().HasMaxLength(64);
            builder.Entity<Deliverable>().Property(d => d.description).IsRequired().HasMaxLength(512);
            builder.Entity<Deliverable>().Property(d => d.developerDescription).IsRequired().HasMaxLength(512);
            builder.Entity<Deliverable>().Property(d => d.createdAt).IsRequired();
            builder.Entity<Deliverable>().Property(d => d.state).IsRequired().HasMaxLength(32);
            builder.Entity<Deliverable>().Property(d => d.file).IsRequired().HasMaxLength(512);
            builder.Entity<Deliverable>().Property(d => d.deadline).IsRequired();
            builder.Entity<Deliverable>().HasOne(d => d.Project)
                .WithMany()
                .HasForeignKey(d => d.projectID);
            builder.Entity<Deliverable>().HasOne(d => d.Developer)
                .WithMany()
                .HasForeignKey(d=> d.developer_id);
            
            //Project Configuration
            builder.Entity<Project>().ToTable("Projects");
            builder.Entity<Project>().HasKey(p => p.projectID);
            builder.Entity<Project>().Property(p => p.projectID).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Project>().HasOne(p => p.Enterprise)
                .WithMany()
                .HasForeignKey(p => p.enterprise_id);
            builder.Entity<Project>().Property(p => p.nameProject).HasMaxLength(100);
            builder.Entity<Project>().Property(p => p.descriptionProject).HasColumnType("TEXT");
            builder.Entity<Project>().Property(p => p.languages).HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(s => s.Trim()).ToList());

            builder.Entity<Project>().Property(p => p.frameworks).HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(s => s.Trim()).ToList());

            builder.Entity<Project>().Property(p => p.budget).HasColumnType("decimal(18, 2)");

            builder.Entity<Project>().Property(p => p.methodologies).HasColumnType("TEXT").HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(s => s.Trim()).ToList());

            builder.Entity<Project>().Property(p => p.developer_id).HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(s => long.Parse(s.Trim())).ToList());
            
            // Configuración de la entidad SupportRequest
            builder.Entity<SupportRequest>().ToTable("SupportRequests");
            builder.Entity<SupportRequest>().HasKey(p => p.Id);
            builder.Entity<SupportRequest>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<SupportRequest>().Property(p => p.UserId).IsRequired();
            builder.Entity<SupportRequest>().Property(p => p.Title).IsRequired().HasMaxLength(100);
            builder.Entity<SupportRequest>().Property(p => p.Description).IsRequired();
            builder.Entity<SupportRequest>().Property(p => p.CreatedAt).IsRequired();
            builder.Entity<SupportRequest>().Property(p => p.Status).IsRequired().HasMaxLength(20);
            builder.Entity<SupportRequest>()
                .HasOne(p => p.User)
                .WithMany(u => u.SupportRequests)
                .HasForeignKey(p => p.UserId);


            builder.Entity<Message>()
                .HasOne(m => m.Sender)
                .WithMany(c => c.SentMessages)
                .HasForeignKey(m => m.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Message>()
                .HasOne(m => m.Receiver)
                .WithMany(c => c.ReceivedMessages)
                .HasForeignKey(m => m.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
    
}