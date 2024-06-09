using System.Text;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using WebmasterAPI.ProjectManagement.Domain.Models;
using Microsoft.IdentityModel.Tokens;
using WebmasterAPI.Authentication.Domain.Repositories;
using WebmasterAPI.Authentication.Domain.Services;
using WebmasterAPI.Authentication.Persistence.Repositories;
using WebmasterAPI.Authentication.Services;
using WebmasterAPI.Shared.Domain.Repositories;
using WebmasterAPI.Shared.Persistence.Contexts;
using WebmasterAPI.Shared.Persistence.Repositories;
using WebmasterAPI.UserManagement.Authorization.Handlers.Implementations;
using WebmasterAPI.UserManagement.Authorization.Handlers.Interface;
using WebmasterAPI.UserManagement.Authorization.Middleware;
using WebmasterAPI.UserManagement.Authorization.Settings;
using WebmasterAPI.UserManagement.Domain.Services;
using WebmasterAPI.UserManagement.Services;
using WebmasterAPI.UserManagement.Authorization.Settings;
using WebmasterAPI.ProjectManagement.Domain.Models;
using WebmasterAPI.ProjectManagement.Domain.Repositories;
using WebmasterAPI.ProjectManagement.Domain.Services;
using WebmasterAPI.ProjectManagement.Domain.Services.Communication;
using WebmasterAPI.ProjectManagement.Domain.Services.Validations;
using WebmasterAPI.ProjectManagement.Mapping;
using WebmasterAPI.ProjectManagement.Persistence.Repositories;
using WebmasterAPI.ProjectManagement.Services;

using WebmasterAPI.Support.Persistence;
using WebmasterAPI.Support.Services;
using WebmasterAPI.Support.Domain.Services;
using WebmasterAPI.Support.Domain.Repositories;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthorization();
builder.Services.AddControllers();

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["AppSettings:Secret"])),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

//Add Database Connection 
builder.Services.AddDbContext<AppDbContext>();

// Add Cors 
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder.WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod());
});

// Add services to the container.
builder.Services.AddControllers();

// Shared Bounded Context Injection Configuration
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Authentication Bounded Context Injection Configuration
builder.Services.AddScoped<IUserService, AuthService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IDeveloperRepository, DeveloperRepository>();
builder.Services.AddScoped<IEnterpriseRepository, EnterpriseRepository>();
builder.Services.AddScoped<IProfileService, ProfileService>();
builder.Services.AddScoped<IPasswordHashingService, PasswordHashingService>();  
builder.Services.AddScoped<IJwtHandler, JwtHandler>();

builder.Services.AddKeyedScoped<ICommonService<ProjectDto, InsertProjectDto, UpdateProjectDto>, ProjectService>("projectService");
builder.Services.AddScoped<IProjectRepository<Project>, ProjectRepository>();
builder.Services.AddScoped<IValidator<InsertProjectDto>, InsertProjectValidation>();
builder.Services.AddScoped<IValidator<UpdateProjectDto>, UpdateProjectValidation>();
builder.Services.AddScoped<ISupportRequestRepository, SupportRequestRepository>();
builder.Services.AddScoped<ISupportRequestService, SupportRequestService>();


// AutoMapper Configuration
builder.Services.AddAutoMapper(
    typeof(WebmasterAPI.Authentication.Mapping.ModelToResourceProfile),
    typeof(WebmasterAPI.Authentication.Mapping.ResourceToModelProfile)
);
builder.Services.AddAutoMapper(typeof(MappingProject));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<JwtMiddleware>();

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.UseCors("AllowSpecificOrigin");

app.MapControllers();

app.Run();
