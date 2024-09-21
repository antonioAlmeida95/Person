using AutoMapper;
using Person.Adpater.Driving.Api.AppService.Interfaces;
using Person.Adpater.Driving.Api.Dtos;
using Person.Core.Domain.Entities;
using Person.Core.Domain.Services;

namespace Person.Adpater.Driving.Api.AppService;

public class PessoaAppService : IPessoaAppService
{
    private readonly IMapper _mapper;
    private readonly IPessoaService _pessoaService;
    private readonly ILogger<PessoaAppService> _logger;

    public PessoaAppService(IMapper mapper,
        IPessoaService pessoaService,
        ILogger<PessoaAppService> logger)
    {
        _mapper = mapper;
        _pessoaService = pessoaService;
        _logger = logger;
    }
    
    public async Task<Guid> CadastrarPessoa(PessoaViewModel pessoaViewModel)
    {
        var pessoa = _mapper.Map<Pessoa>(pessoaViewModel);
        var pessoaId = await _pessoaService.CadastrarPessoa(pessoa);
        return pessoaId;
    }

    public async Task<PessoaViewModel?> ObterPessoaPorId(Guid pessoaId)
    {
        var pessoa = await _pessoaService.ObterPessoaPorId(pessoaId);
        return _mapper.Map<PessoaViewModel>(pessoa);
    }

    public async Task<bool> AtualizarPessoa(PessoaViewModel pessoaViewModel)
    {
        var pessoa = _mapper.Map<Pessoa>(pessoaViewModel);
        var pessoaAtualizada = await _pessoaService.AtualizarPessoa(pessoa);
        return pessoaAtualizada;
    }

    public async Task<bool> RemoverPessoa(Guid pessoaId)
    {
        var pessoaRemovida = await _pessoaService.RemoverPessoa(pessoaId);
        return pessoaRemovida;
    }
}