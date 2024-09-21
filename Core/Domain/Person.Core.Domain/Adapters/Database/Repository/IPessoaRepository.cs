using System.Linq.Expressions;
using Person.Core.Domain.Entities;

namespace Person.Core.Domain.Adapters.Database.Repository;

public interface IPessoaRepository
{
    Task<bool> AdicionarPessoaAsync(Pessoa pessoa);
    Task<bool> AtualizarPessoa(Pessoa pessoa);
    Task<Pessoa?> ObterPessoaPorFiltro(Expression<Func<Pessoa, bool>> predicate, bool track = false);
}