using Microsoft.EntityFrameworkCore;
using SGE.Application.Interfaces.Repositories;
using SGE.Application.Interfaces.Services;
using SGE.Application.Mappings;
using SGE.Application.Services;
using SGE.Infrastructure.Data;
using SGE.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Récupérer la chaine de connexion
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Ajouter DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddAutoMapper(cfg => { }, typeof(MappingProfile).Assembly);

// Add services to the container.
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();