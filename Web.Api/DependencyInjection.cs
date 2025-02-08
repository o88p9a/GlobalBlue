using GlobalBlue.ErrorHandler;
using Microsoft.AspNetCore.Diagnostics;

namespace GlobalBlue;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("AllowLocalhost",
                builder => builder.SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost")
                    .AllowAnyMethod()
                    .AllowAnyHeader());
        });
        services.AddControllers();

        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddProblemDetails();

        return services;
    }
}
