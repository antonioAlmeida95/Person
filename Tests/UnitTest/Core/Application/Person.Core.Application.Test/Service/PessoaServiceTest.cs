using System.Linq.Expressions;
using Moq;
using Notification.Core.Domain.Constantes;
using Person.Application.Service;
using Person.Core.Domain.Adapters.Database.UnitOfWork;
using Person.Core.Domain.Adapters.Integrations.Producer.Interface;
using Person.Core.Domain.Adapters.Integrations.Producer.Model;
using Person.Core.Domain.Entities;
using Person.Core.Domain.Entities.Enums;

namespace Person.Core.Application.Test.Service;

[Collection(nameof(PessoaServiceCollection))]
public class PessoaServiceTest
{
    private readonly PessoaService _pessoaService;
    private readonly PessoaServiceFixture _fixture;

    public PessoaServiceTest(PessoaServiceFixture pessoaServiceFixture)
    {
        _fixture = pessoaServiceFixture;
        _pessoaService = _fixture.ObterPessoaService();
    }
    
    [Fact]
    public async Task CadastrarPessoa_CadastraPessoaSucesso()
    {
        //Arrange
        var pessoa = PessoaFactory.CriarPessoa("Lucas", "lucas@mail.com", TipoPessoa.Fisica,
            "000000", Guid.NewGuid());
        
        //Setup
        _fixture.SetupAdicionarPessoaAsync();
        
        //Act
        var pessoaId = await _pessoaService.CadastrarPessoa(pessoa);
        
        //Assert
        Assert.True(pessoaId != Guid.Empty);
        
        _fixture.Mocker.GetMock<IUnitOfWork>()
            .Verify(s => s.Pessoa.AdicionarPessoaAsync(It.Is<Pessoa>(p => p.Id == pessoa.Id
                && p.Documento == pessoa.Documento
                && p.Email == pessoa.Email
                && p.Nome == pessoa.Nome)),
                Times.Once);
        
        _fixture.Mocker.GetMock<IProducerService>()
            .Verify(s => s.ProduceMessage(It.Is<Message>(m =>
                    m.Nome == pessoa.Nome &&
                    m.NotificationId == NotificationIdentify.NOTIFICACAO_NOVO_USUARIO())),
                Times.Once);
    }

    [Fact]
    public async Task CadastrarPessoa_CadastraPessoaFalha()
    {
        //Arrange
        var pessoa = PessoaFactory.CriarPessoa();
        
        //Setup
        
        //Act
        var pessoaId = await _pessoaService.CadastrarPessoa(pessoa);
        
        //Assert
        Assert.True(pessoaId == Guid.Empty);
        
        _fixture.Mocker.GetMock<IUnitOfWork>()
            .Verify(s => s.Pessoa.AdicionarPessoaAsync(It.IsAny<Pessoa>()),
                Times.Never);
        
        _fixture.Mocker.GetMock<IProducerService>()
            .Verify(s => s.ProduceMessage(It.IsAny<Message>()),
                Times.Never);
    }
    
    [Fact]
    public async Task ObterPessoaPorId_ObterPessoaSucesso()
    {
        //Arrange
        var pessoaId = Guid.NewGuid();
        var pessoa = PessoaFactory.CriarPessoa("Lucas", "lucas@mail.com", TipoPessoa.Fisica,
            "000000", Guid.NewGuid());
        
        //Setup
        _fixture.SetupObterPessoaPorId(pessoa);
        
        //Act
        var response = await _pessoaService.ObterPessoaPorId(pessoaId);
        
        //Assert
        Assert.NotNull(response);
        Assert.IsAssignableFrom<Pessoa>(pessoa);
        
        _fixture.Mocker.GetMock<IUnitOfWork>()
            .Verify(s => s.Pessoa.ObterPessoaPorFiltro(It.IsAny<Expression<Func<Pessoa, bool>>>(),
                    It.Is<bool>(t => !t)),
                Times.Once);
    }
    
    [Fact]
    public async Task AtualizarPessoa_AtualizaPessoaSucesso()
    {
        //Arrange
        var pessoa = PessoaFactory.CriarPessoa("Lucas", "lucas@mail.com", TipoPessoa.Fisica,
            "000000", Guid.NewGuid());
        
        //Setup
        _fixture.SetupAtualizarPessoaAsync();
        _fixture.SetupObterPessoaPorId(pessoa);
        
        //Act
        var sucesso = await _pessoaService.AtualizarPessoa(pessoa);
        
        //Assert
        Assert.True(sucesso);
        
        _fixture.Mocker.GetMock<IUnitOfWork>()
            .Verify(s => s.Pessoa.ObterPessoaPorFiltro(It.IsAny<Expression<Func<Pessoa, bool>>>(),
                    It.Is<bool>(t => !t)),
                Times.Once);
        
        _fixture.Mocker.GetMock<IUnitOfWork>()
            .Verify(s => s.Pessoa.AtualizarPessoaAsync(It.Is<Pessoa>(p => p.Id == pessoa.Id
                                                                          && p.Documento == pessoa.Documento
                                                                          && p.Email == pessoa.Email
                                                                          && p.Nome == pessoa.Nome)),
                Times.Once);
        
        _fixture.Mocker.GetMock<IProducerService>()
            .Verify(s => s.ProduceMessage(It.Is<Message>(
                    m => m.Nome == pessoa.Nome &&
                         m.NotificationId == NotificationIdentify.NOTIFICACAO_USUARIO_ATUALIZADO())),
                Times.Once);
    }

    [Fact]
    public async Task AtualizarPessoa_AtualizaPessoaFalha()
    {
        //Arrange
        var pessoa = PessoaFactory.CriarPessoa();
        
        //Setup
        
        //Act
        var sucesso = await _pessoaService.AtualizarPessoa(pessoa);
        
        //Assert
        Assert.False(sucesso);
        
        _fixture.Mocker.GetMock<IUnitOfWork>()
            .Verify(s => s.Pessoa.AtualizarPessoaAsync(It.IsAny<Pessoa>()),
                Times.Never);
        
        _fixture.Mocker.GetMock<IProducerService>()
            .Verify(s => s.ProduceMessage(It.IsAny<Message>()),
                Times.Never);
    }
    
    //RemoverPessoa_sucesso
    [Fact]
    public async Task RemoverPessoa_RemovePessoaSucesso()
    {
        //Arrange
        var pessoaId = Guid.NewGuid();
        var pessoa = PessoaFactory.CriarPessoa("Lucas", "lucas@mail.com", TipoPessoa.Fisica,
            "000000", Guid.NewGuid());
        
        //Setup
        _fixture.SetupObterPessoaPorId(pessoa);
        _fixture.SetupAtualizarPessoaAsync();
        
        //Act
        var sucesso = await _pessoaService.RemoverPessoa(pessoaId);
        
        //Assert
        Assert.True(sucesso);
        
        _fixture.Mocker.GetMock<IUnitOfWork>()
            .Verify(s => s.Pessoa.ObterPessoaPorFiltro(It.IsAny<Expression<Func<Pessoa, bool>>>(),
                    It.Is<bool>(t => t)),
                Times.Once);
        
        _fixture.Mocker.GetMock<IUnitOfWork>()
            .Verify(s => s.Pessoa.AtualizarPessoaAsync(It.Is<Pessoa>(p => p.Id == pessoa.Id
                                                                          && p.Documento == pessoa.Documento
                                                                          && p.Email == pessoa.Email
                                                                          && p.Nome == pessoa.Nome
                                                                          && !p.Status)),
                Times.Once);
        
        _fixture.Mocker.GetMock<IProducerService>()
            .Verify(s => s.ProduceMessage(It.Is<Message>(
                    m => m.Nome == pessoa.Nome &&
                         m.NotificationId == NotificationIdentify.NOTIFICACAO_USUARIO_REMOVIDO())),
                Times.Once);
    }

    [Fact]
    public async Task RemoverPessoa_RemovePessoaFalha()
    {
        //Arrange
        var pessoaId = Guid.NewGuid();
        
        //Setup
        _fixture.SetupObterPessoaPorId(null);
        
        //Act
        var sucesso = await _pessoaService.RemoverPessoa(pessoaId);
        
        //Assert
        Assert.False(sucesso);
        
        _fixture.Mocker.GetMock<IUnitOfWork>()
            .Verify(s => s.Pessoa.ObterPessoaPorFiltro(It.IsAny<Expression<Func<Pessoa, bool>>>(),
                    It.Is<bool>(t => t)),
                Times.Once);
        
        _fixture.Mocker.GetMock<IUnitOfWork>()
            .Verify(s => s.Pessoa.AtualizarPessoaAsync(It.IsAny<Pessoa>()),
                Times.Never);
        
        _fixture.Mocker.GetMock<IProducerService>()
            .Verify(s => s.ProduceMessage(It.IsAny<Message>()),
                Times.Never);
    }
}