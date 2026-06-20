using Application.Ports.Inbound;
using Application.Ports.Outbound.Repository;
using Application.Ports.Outbound.Security;
using Application.UseCases;
using Infrastructure.Adapters.Outbound.Database;
using Infrastructure.Adapters.Outbound.Repository;
using Infrastructure.Adapters.Outbound.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Configuration;

public static class IoC
{
    public static void RegisterInfrastructureDependencies(this IServiceCollection service, string dbName)
    {
        service.AddTransient<IUserRepository, UserRepository>();
        
        service.AddDbContext<MyDatabase>(options => options.UseSqlite(BuildConnectionString(dbName)));
    }

    public static void RegisterApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<ICreateUserUseCase, CreateUserUseCase>();
        services.AddScoped<IPasswordHasher, Rfc2898DeriveBytesPasswordHasher>();
    }
   
    private static string BuildConnectionString(string dbName)
    {
        if (!string.IsNullOrWhiteSpace(dbName))
        {
            throw new InvalidOperationException(
                $"The database '{dbName}' is not configured.");
        }

        var dbPath = Path.Combine(Directory.GetCurrentDirectory(), "Infrastructure", "Adapters", "Outbound", "Database", dbName);

        Directory.CreateDirectory(Path.GetDirectoryName(dbPath)!);
        return $"Data Source={dbPath}";
    }
}