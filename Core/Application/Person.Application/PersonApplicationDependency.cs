using Microsoft.Extensions.DependencyInjection;
using Person.Application.Service;
using Person.Core.Domain.Services;

namespace Person.Application;

public static class PersonApplicationDependency
{
    public static void AddPersonApplicationModule(this IServiceCollection services)
    {
        services.AddScoped<IPessoaService, PessoaService>();
    }
}