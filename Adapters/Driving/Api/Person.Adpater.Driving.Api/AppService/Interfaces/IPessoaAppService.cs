using Person.Adpater.Driving.Api.Dtos;

namespace Person.Adpater.Driving.Api.AppService.Interfaces;

public interface IPessoaAppService
{
    Task<Guid> CadastrarPessoa(PessoaViewModel pessoaViewModel);

    Task<PessoaViewModel?> ObterPessoaPorId(Guid pessoaId);

    Task<bool> AtualizarPessoa(PessoaViewModel pessoaViewModel);

    Task<bool> RemoverPessoa(Guid pessoaId);
}