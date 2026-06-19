using ColorexApp.Application.Abstractions.Auth;
using ColorexApp.Application.Abstractions.Data;
using ColorexApp.Application.Abstractions.Repositories;
using ColorexApp.Infrastructure.Auth;
using ColorexApp.Infrastructure.Persistence;
using ColorexApp.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ColorexApp.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("PostgreSQL");
        services.AddDbContext<ColorexDbContext>(options => options.UseNpgsql(connectionString));
        services.AddScoped<IDbContext>(provider => provider.GetRequiredService<ColorexDbContext>());
        services.AddScoped<IJwtProvider, JwtProvider>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IBranchRepository, BranchRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IInventoryTransferRepository, InventoryTransferRepository>();
        services.AddScoped<IInvoiceRepository, InvoiceRepository>();
        services.AddScoped<IBranchInventoryRepository, BranchInventoryRepository>();
        return services;
    }
}
