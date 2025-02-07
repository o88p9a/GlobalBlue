using Application;
using GlobalBlue;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplication()
    .AddPresentation();

builder.Services.AddControllers();

var app = builder.Build();


app.UseExceptionHandler();
app.MapControllers();

app.Run();