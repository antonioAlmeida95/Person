using Person.Adpater.Driving.Api.AppService;
using Person.Adpater.Driving.Api.AppService.Interfaces;
using Person.Adpater.Driving.Api.Mappings;

namespace Person.Adpater.Driving.Api;

public static class PersonApiDependency
{
    public static void AddPersonApiModule(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(AutoMapperConfiguration));
        services.AddScoped<IPessoaAppService, PessoaAppService>();
    }
}