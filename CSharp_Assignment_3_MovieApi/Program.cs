using CSharp_Assignment_3_MovieApi.DatabaseContext;
using CSharp_Assignment_3_MovieApi.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Linq.Expressions;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<ICharacterService, CharacterService>();
builder.Services.AddTransient<IFranchiseService, FranchiseService>();
builder.Services.AddTransient<IMovieService, MovieService>();
// Add controllers and database context to the container
builder.Services.AddControllers();
builder.Services.AddDbContext<MovieDbContext>();

// Add AutoMapper and Swagger to the container
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Set up Swagger documentation
var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Version = "v1",
        Title = "Movie API",
        Description = "This API provides access to information about movies, franchises, and characters. It allows users to retrieve, create, update, and delete data, as well as search for movies and franchises based on various criteria.",
        Contact = new OpenApiContact
        {
            Name = "Mads Ohmsen, Thomas Osterhammel",
            Url = new Uri("https://github.com/ohmsen93/CSharp_Assignment_3_MovieApi")
        },
        License = new OpenApiLicense
        {
            Name = "MIT 2022",
            Url = new Uri("https://opensource.org/license/mit/")
        }
    });
    options.IncludeXmlComments(xmlPath);
});

// Build the application.
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Use Swagger UI in development environment
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Set up database migration
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var dbContext = services.GetRequiredService<MovieDbContext>();
//dbContext.Database.EnsureCreated(); 
dbContext.Database.Migrate();

// Set up HTTPS redirection and authorization
app.UseHttpsRedirection();
app.UseAuthorization();

// Map the controllers to HTTP endpoints
app.MapControllers();

// Run the application.
app.Run();


