using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using Person.Core.Domain.Adapters.Database.UnitOfWork;
using Person.Core.Domain.Adapters.Integrations.Producer.Interface;
using Person.Core.Domain.Adapters.Integrations.Producer.Model;
using Person.Core.Domain.Entities;
using Person.Core.Domain.Services;

namespace Person.Application.Service;

public class PessoaService : IPessoaService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IProducerService _producerService;
    private readonly ILogger<PessoaService> _logger;

    private const string AppOrigem = "PSN-APP";
    
    public PessoaService(IUnitOfWork unitOfWork,
        ILogger<PessoaService> logger,
        IProducerService producerService)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _producerService = producerService;
    }
    public async Task<Guid> CadastrarPessoa(Pessoa pessoa)
    {
        if (!pessoa.ValidarEntidade())
        {
            LogErrosEntidade(pessoa);
            return Guid.Empty;
        }
        
        var commit = await _unitOfWork.Pessoa.AdicionarPessoaAsync(pessoa);

        if (commit)
            await SendNotificationProducer(pessoa.Nome, pessoa.Email, Guid.NewGuid());
        
        return commit ? pessoa.Id : Guid.Empty;
    }

    public async Task<Pessoa?> ObterPessoaPorId(Guid pessoaId) =>
        await _unitOfWork.Pessoa.ObterPessoaPorFiltro(p => p.Id == pessoaId);

    public async Task<bool> AtualizarPessoa(Pessoa pessoa)
    {
        if (!pessoa.ValidarEntidade())
        {
            LogErrosEntidade(pessoa);
            return false;
        }
        
        var commit = await _unitOfWork.Pessoa.AtualizarPessoaAsync(pessoa);

        if (commit)
            await SendNotificationProducer(pessoa.Nome, pessoa.Email, Guid.NewGuid());
        
        return commit;
    }

    public async Task<bool> RemoverPessoa(Guid pessoaId)
    {
        var pessoa = await _unitOfWork.Pessoa.ObterPessoaPorFiltro(p => p.Id == pessoaId, true);
        if (pessoa is null)
        {
            _logger.LogError("Dados invalidos para remoção");
            return false;
        }
        
        pessoa.AlterarStatus(false);
        var commit = await _unitOfWork.Pessoa.AtualizarPessoaAsync(pessoa);
        
        if (commit)
            await SendNotificationProducer(pessoa.Nome, pessoa.Email, Guid.NewGuid());

        return commit;
    }

    private void LogErrosEntidade(Pessoa pessoa)
    {
        _logger.LogError("Dados da Pessoa Invalida:");
        foreach (var erro in pessoa.ValidationResult.Errors.OfType<ValidationFailure>())
            _logger.LogError(erro.ErrorMessage);
    }

    private async Task SendNotificationProducer(string nome, string email, Guid notificationId)
    {
        var message = CriarMessage(nome, email, notificationId);
        await _producerService.ProduceMessage(message);
    }

    private static Message CriarMessage(string nome, string email, Guid notificationId) => new()
    {
        Origem = AppOrigem,
        Email = email,
        Nome = nome,
        NotificationId = notificationId
    };
}