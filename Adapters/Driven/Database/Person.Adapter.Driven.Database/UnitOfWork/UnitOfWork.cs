using Microsoft.Extensions.DependencyInjection;
using Person.Core.Domain.Adapters.Database.Repository;
using Person.Core.Domain.Adapters.Database.UnitOfWork;

namespace Person.Adapter.Driven.Database.UnitOfWork;

public class UnitOfWork(IServiceProvider serviceProvider) : IUnitOfWork
{
    public IPessoaRepository Pessoa => serviceProvider.GetRequiredService<IPessoaRepository>();
}