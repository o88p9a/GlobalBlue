using GlobalBlue.ErrorHandler;
using Microsoft.AspNetCore.Diagnostics;

namespace GlobalBlue;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddControllers();

        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddProblemDetails();

        return services;
    }
}
