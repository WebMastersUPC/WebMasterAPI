using FluentValidation;
using Microsoft.EntityFrameworkCore;
using WebmasterAPI.ApiProject.Domain.Models;
using WebmasterAPI.Authentication.Domain.Repositories;
using WebmasterAPI.Authentication.Domain.Services;
using WebmasterAPI.Authentication.Persistence.Repositories;
using WebmasterAPI.Authentication.Services;
using WebmasterAPI.ApiProject.Domain.Repositories;
using WebmasterAPI.ApiProject.Domain.Services;
using WebmasterAPI.ApiProject.Domain.Services.Communication;
using WebmasterAPI.ApiProject.Domain.Services.Validations;
using WebmasterAPI.ApiProject.Mapping;
using WebmasterAPI.ApiProject.Persistence.Repositories;
using WebmasterAPI.ApiProject.Services;
using WebmasterAPI.Shared.Domain.Repositories;
using WebmasterAPI.Shared.Persistence.Contexts;
using WebmasterAPI.Shared.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthorization();
builder.Services.AddControllers();

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

builder.Services.AddKeyedScoped<ICommonService<ProjectDto, InsertProjectDto, UpdateProjectDto>, ProjectService>("projectService");
builder.Services.AddScoped<IProjectRepository<Project>, ProjectRepository>();
builder.Services.AddScoped<IValidator<InsertProjectDto>, InsertProjectValidation>();
builder.Services.AddScoped<IValidator<UpdateProjectDto>, UpdateProjectValidation>();

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

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors("AllowSpecificOrigin");

app.UseAuthorization();

app.MapControllers();

app.Run();
