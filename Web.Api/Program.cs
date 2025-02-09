using Application;
using GlobalBlue;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplication()
    .AddPresentation();

var app = builder.Build();

app.UseCors("AllowLocalhost");
app.UseExceptionHandler();
app.MapControllers();

app.Run();