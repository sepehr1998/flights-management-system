using FlightsManagementBackend.Data;
using FlightsManagementBackend.Domain.Entities;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<Entities>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Flights")));

builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.AddServer(new OpenApiServer
    {
        Description = "Development Server",
        Url = "https://localhost:7241"
    });
    c.CustomOperationIds(e => $"{e.ActionDescriptor.RouteValues["action"] + e.ActionDescriptor.RouteValues["controller"]}");
});

builder.Services.AddSingleton<Entities>();

var app = builder.Build();

var entities = app.Services.CreateScope().ServiceProvider.GetService<Entities>();

entities.Database.EnsureCreated();

var random = new Random();
Flight[] flightsToSeed = new Flight[] {
    new(Guid.NewGuid(),
        "American Airlines",
        random.Next(90, 5000).ToString(),
        new TimePlace("Los Angeles", DateTime.Now.AddHours(random.Next(1, 3))),
        new TimePlace("Istanbul", DateTime.Now.AddHours(random.Next(4, 10))),
        random.Next(1, 853)),
    new(Guid.NewGuid(),
        "Deutsche BA",
        random.Next(90, 5000).ToString(),
        new TimePlace("Munchen", DateTime.Now.AddHours(random.Next(1, 10))),
        new TimePlace("Schiphol", DateTime.Now.AddHours(random.Next(4, 15))),
        random.Next(1, 853)),
    new(Guid.NewGuid(),
        "British Airways",
        random.Next(90, 5000).ToString(),
        new TimePlace("London, England", DateTime.Now.AddHours(random.Next(1, 15))),
        new TimePlace("Vizzola-Ticino", DateTime.Now.AddHours(random.Next(4, 18))),
        random.Next(1, 853)),
    new(Guid.NewGuid(),
        "Basiq Air",
        random.Next(90, 5000).ToString(),
        new TimePlace("Amsterdam", DateTime.Now.AddHours(random.Next(1, 21))),
        new TimePlace("Glasgow, Scotland", DateTime.Now.AddHours(random.Next(4, 21))),
        random.Next(1, 853)),
    new(Guid.NewGuid(),
        "BB Heliag",
        random.Next(90, 5000).ToString(),
        new TimePlace("Zurich", DateTime.Now.AddHours(random.Next(1, 23))),
        new TimePlace("Baku", DateTime.Now.AddHours(random.Next(4, 25))),
        random.Next(1, 853)),
    new(Guid.NewGuid(),
        "Adria Airways",
        random.Next(90, 5000).ToString(),
        new TimePlace("Ljubljana", DateTime.Now.AddHours(random.Next(1, 15))),
        new TimePlace("Warsaw", DateTime.Now.AddHours(random.Next(4, 19))),
        random.Next(1, 853)),
    new(Guid.NewGuid(),
        "ABA Air",
        random.Next(90, 5000).ToString(),
        new TimePlace("Praha Ruzyne", DateTime.Now.AddHours(random.Next(1, 55))),
        new TimePlace("Paris", DateTime.Now.AddHours(random.Next(4, 58))),
        random.Next(1, 853)),
    new(Guid.NewGuid(),
        "AB Corporate Aviation",
        random.Next(90, 5000).ToString(),
        new TimePlace("Le Bourget", DateTime.Now.AddHours(random.Next(1, 58))),
        new TimePlace("Zagreb", DateTime.Now.AddHours(random.Next(4, 60))),
        random.Next(1, 853))
};

entities.Flights.AddRange(flightsToSeed);
entities.SaveChanges();

app.UseCors(b => b.WithOrigins("*")
    .AllowAnyHeader()
    .AllowAnyMethod());

app.MapControllers();
app.UseSwagger().UseSwaggerUI();
app.MapGet("/", () => "Hello World!");

app.Run();