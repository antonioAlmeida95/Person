using AutoFixture;
using Moq;
using Person.Adpater.Driving.Api.AppService;
using Person.Adpater.Driving.Api.Dtos;
using Person.Core.Domain.Entities;
using Person.Core.Domain.Entities.Enums;
using Person.Core.Domain.Services;

namespace Person.Adapters.Api.Test.AppService;

[Collection(nameof(PessoaAppServiceCollection))]
public class PessoaAppServiceTest
{
    private readonly PessoaAppService _pessoaAppService;
    private readonly PessoaAppServiceFixture _fixture;

    public PessoaAppServiceTest(PessoaAppServiceFixture fixture)
    {
        _fixture = fixture;
        _pessoaAppService = _fixture.ObterPessoaAppService();
    }
    
    [Fact]
    public async Task CadastrarPessoa_Sucesso()
    {
        //Arrange
        var pessoaId = Guid.NewGuid();
        var viewModel = new Fixture()
            .Build<PessoaViewModel>()
            .Create();
        
        //Setup
        _fixture.SetupCadastrarPessoa(pessoaId);
        
        //Act
        var response = await _pessoaAppService.CadastrarPessoa(viewModel);
        
        //Assert
        Assert.Equal(pessoaId, response);
        
        _fixture.Mocker.GetMock<IPessoaService>()
            .Verify(s => s.CadastrarPessoa(It.Is<Pessoa>(p => p.Nome == viewModel.Nome
                && p.Email == viewModel.Email
                && p.Documento == viewModel.Documento)),
                Times.Once);
    }
    
    [Fact]
    public async Task ObterPessoaPorId_Sucesso()
    {
        //Arrange
        var pessoaId = Guid.NewGuid();
        var pessoa = PessoaFactory.CriarPessoa("Lucas", "lucas@mail.com", TipoPessoa.Fisica,
            "000000", pessoaId);
        
        //Setup
        _fixture.SetupObterPessoaPorId(pessoa);
        
        //Act
        var response = await _pessoaAppService.ObterPessoaPorId(pessoaId);
        
        //Assert
        Assert.NotNull(response);
        Assert.Equal(pessoaId, response.PessoaId);
        
        _fixture.Mocker.GetMock<IPessoaService>()
            .Verify(s => s.ObterPessoaPorId(It.Is<Guid>(p => p == pessoaId)),
                Times.Once);
    }
    
    [Fact]
    public async Task ObterPessoaPorId_Falha()
    {
        //Arrange
        var pessoaId = Guid.NewGuid();
        
        //Setup
        _fixture.SetupObterPessoaPorId(null);
        
        //Act
        var response = await _pessoaAppService.ObterPessoaPorId(pessoaId);
        
        //Assert
        Assert.Null(response);
        
        _fixture.Mocker.GetMock<IPessoaService>()
            .Verify(s => s.ObterPessoaPorId(It.Is<Guid>(p => p == pessoaId)),
                Times.Once);
    }
    
    [Fact]
    public async Task AtualizarPessoa_Sucesso()
    {
        //Arrange
        var viewModel = new Fixture()
            .Build<PessoaViewModel>()
            .Create();
        
        //Setup
        _fixture.SetupAtualizarPessoa();
        
        //Act
        var response = await _pessoaAppService.AtualizarPessoa(viewModel);
        
        //Assert
        Assert.True(response);
        
        _fixture.Mocker.GetMock<IPessoaService>()
            .Verify(s => s.AtualizarPessoa(It.Is<Pessoa>(p => p.Nome == viewModel.Nome
                                                              && p.Email == viewModel.Email
                                                              && p.Documento == viewModel.Documento)),
                Times.Once);
    }
    
    [Fact]
    public async Task RemoverPessoa_Sucesso()
    {
        //Arrange
        var pessoaId = Guid.NewGuid();
        
        //Setup
        _fixture.SetupRemoverPessoa();
        
        //Act
        var response = await _pessoaAppService.RemoverPessoa(pessoaId);
        
        //Assert
        Assert.True(response);
        
        _fixture.Mocker.GetMock<IPessoaService>()
            .Verify(s => s.RemoverPessoa(It.Is<Guid>(p => p == pessoaId)),
                Times.Once);
    }
}