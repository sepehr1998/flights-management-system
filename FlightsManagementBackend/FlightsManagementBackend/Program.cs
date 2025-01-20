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
});

var app = builder.Build();

app.MapControllers();
app.UseSwagger().UseSwaggerUI();
app.MapGet("/", () => "Hello World!");

app.Run();