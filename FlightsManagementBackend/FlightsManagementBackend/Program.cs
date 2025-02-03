using FlightsManagementBackend.Data;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

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

app.UseCors(b => b.WithOrigins("*")
    .AllowAnyHeader()
    .AllowAnyMethod());

app.MapControllers();
app.UseSwagger().UseSwaggerUI();
app.MapGet("/", () => "Hello World!");

app.Run();