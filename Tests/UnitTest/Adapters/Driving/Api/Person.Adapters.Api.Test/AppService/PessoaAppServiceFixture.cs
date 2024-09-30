using Moq;
using Moq.AutoMock;
using Person.Adpater.Driving.Api.AppService;
using Person.Adpater.Driving.Api.Mappings;
using Person.Core.Domain.Entities;
using Person.Core.Domain.Services;

namespace Person.Adapters.Api.Test.AppService;

[CollectionDefinition(nameof(PessoaAppServiceCollection))]
public class PessoaAppServiceCollection : ICollectionFixture<PessoaAppServiceFixture> { }
public class PessoaAppServiceFixture
{
    public AutoMocker Mocker;
    private PessoaAppService _pessoaAppService;

    public PessoaAppService ObterPessoaAppService()
    {
        Mocker = new AutoMocker();
        Mocker.Use(AutoMapperConfiguration.RegisterMappings().CreateMapper());

        _pessoaAppService = Mocker.CreateInstance<PessoaAppService>();

        return _pessoaAppService;
    }

    public void SetupCadastrarPessoa(Guid pessoaId)
    {
        Mocker.GetMock<IPessoaService>()
            .Setup(s => s.CadastrarPessoa(It.IsAny<Pessoa>()))
            .ReturnsAsync(pessoaId);
    }
    
    public void SetupObterPessoaPorId(Pessoa? pessoa)
    {
        Mocker.GetMock<IPessoaService>()
            .Setup(s => s.ObterPessoaPorId(It.IsAny<Guid>()))
            .ReturnsAsync(pessoa);
    }
    
    public void SetupAtualizarPessoa(bool sucesso = true)
    {
        Mocker.GetMock<IPessoaService>()
            .Setup(s => s.AtualizarPessoa(It.IsAny<Pessoa>()))
            .ReturnsAsync(sucesso);
    }
    
    public void SetupRemoverPessoa(bool sucesso = true)
    {
        Mocker.GetMock<IPessoaService>()
            .Setup(s => s.RemoverPessoa(It.IsAny<Guid>()))
            .ReturnsAsync(sucesso);
    }
}