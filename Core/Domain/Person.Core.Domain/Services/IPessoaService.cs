using Person.Core.Domain.Entities;

namespace Person.Core.Domain.Services;

public interface IPessoaService
{
    Task<Guid> CadastrarPessoa(Pessoa pessoa);

    Task<Pessoa?> ObterPessoaPorId(Guid pessoaId);

    Task<bool> AtualizarPessoa(Pessoa pessoa);

    Task<bool> RemoverPessoa(Guid pessoaId);
}