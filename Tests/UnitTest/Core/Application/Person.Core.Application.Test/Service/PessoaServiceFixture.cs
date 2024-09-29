using System.Linq.Expressions;
using Moq;
using Moq.AutoMock;
using Person.Application.Service;
using Person.Core.Domain.Adapters.Database.UnitOfWork;
using Person.Core.Domain.Entities;

namespace Person.Core.Application.Test.Service;

[CollectionDefinition(nameof(PessoaServiceCollection))]
public class PessoaServiceCollection : ICollectionFixture<PessoaServiceFixture> { }
public class PessoaServiceFixture
{
    public AutoMocker Mocker;
    private PessoaService _pessoaService;
    
    public PessoaService ObterPessoaService()
    {
        Mocker = new AutoMocker();

       _pessoaService = Mocker.CreateInstance<PessoaService>();

        return _pessoaService;
    }

    public void SetupAdicionarPessoaAsync(bool commited = true) =>
        Mocker.GetMock<IUnitOfWork>()
            .Setup(s => s.Pessoa.AdicionarPessoaAsync(It.IsAny<Pessoa>()))
            .ReturnsAsync(commited);

    public void SetupAtualizarPessoaAsync(bool commited = true) =>
        Mocker.GetMock<IUnitOfWork>()
            .Setup(s => s.Pessoa.AtualizarPessoaAsync(It.IsAny<Pessoa>()))
            .ReturnsAsync(commited);

    public void SetupObterPessoaPorId(Pessoa? pessoa) =>
        Mocker.GetMock<IUnitOfWork>()
            .Setup(s => s.Pessoa.ObterPessoaPorFiltro(It.IsAny<Expression<Func<Pessoa, bool>>>(), It.IsAny<bool>()))
            .ReturnsAsync(pessoa);
}