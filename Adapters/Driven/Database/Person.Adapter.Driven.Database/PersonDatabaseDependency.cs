using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Person.Adapter.Driven.Database.Repository;
using Person.Adapter.Driven.Database.UnitOfWork;
using Person.Core.Domain.Adapters.Database.Repository;
using Person.Core.Domain.Adapters.Database.UnitOfWork;
using UnitOfWorkInstance = Person.Adapter.Driven.Database.UnitOfWork.UnitOfWork;

namespace Person.Adapter.Driven.Database;

public static class PersonDatabaseDependency
{
    public static void AddPersonDatabaseModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IUnitOfWork, UnitOfWorkInstance>();
        services.AddScoped(sp =>
        {
            var connectionString = GetConnectionString(configuration);
            var context = new PessoaContext(connectionString);
            
            if (context.Database.GetPendingMigrations().Any())
                context.Database.Migrate();
            
            return context;
        });

        services.AddScoped<IPessoaRepository, PessoaRepository>();
    }

    private static string? GetConnectionString(IConfiguration configuration)
    {
        var connectionStringName = "DefaultConnection";
        #if !DEBUG
        connectionStringName = "DockerConnection";
        #endif

        return configuration.GetConnectionString(connectionStringName);
    }
}