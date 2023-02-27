using CSharp_Assignment_3_MovieApi.DatabaseContext;
using Microsoft.OpenApi.Models;
using System.Linq.Expressions;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<MovieDbContext>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Version = "v1",
        Title = "(yet another) Movie API",
        Description = "yet another movie api",
        Contact = new OpenApiContact
        {
            Name = "Thomas",
            Url = new Uri("https://github.com/thomas")
        },
        License = new OpenApiLicense
        {
            Name = "MIT 2022",
            Url = new Uri("https://opensource.org/license/mit/")
        }
    });
    //options.add
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Build the application.

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var dbContext = services.GetRequiredService<MovieDbContext>();
dbContext.Database.EnsureCreated(); //


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


