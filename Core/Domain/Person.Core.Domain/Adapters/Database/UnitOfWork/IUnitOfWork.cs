using Person.Core.Domain.Adapters.Database.Repository;

namespace Person.Core.Domain.Adapters.Database.UnitOfWork;

public interface IUnitOfWork
{
    IPessoaRepository Pessoa { get; }
}