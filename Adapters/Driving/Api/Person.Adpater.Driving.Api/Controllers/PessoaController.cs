using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Person.Adpater.Driving.Api.AppService.Interfaces;
using Person.Adpater.Driving.Api.Dtos;

namespace Person.Adpater.Driving.Api.Controllers;

[ApiController]
[Route("Pessoa")]
public class PessoaController : ControllerBase
{
    private readonly IPessoaAppService _pessoaAppService;

    public PessoaController(IPessoaAppService pessoaAppService)
    {
        _pessoaAppService = pessoaAppService;
    }
    
    /// <summary>
    ///     Endpoint para obtenção da pessoa por id
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Dados da Pessoa</returns>
    [HttpGet]
    [Route("PorId/{id:guid}")]
    [ProducesResponseType(typeof(Ok<PessoaViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFound), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        var response = await _pessoaAppService.ObterPessoaPorId(id);
        return response is not null ? Ok(response) : NotFound();
    }

    /// <summary>
    ///     Endpoint para cadastro de uma pessoa
    /// </summary>
    /// <param name="pessoaViewModel">Dados da pessoa</param>
    /// <returns>Identificador da Pessoa cadastrada</returns>
    [HttpPost]
    [Route("Cadastrar")]
    [ProducesResponseType(typeof(Ok<Guid>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BadRequest), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Post([FromBody] PessoaViewModel pessoaViewModel)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState.Values);

        var pessoaId = await _pessoaAppService.CadastrarPessoa(pessoaViewModel);

        return pessoaId != Guid.Empty ? Ok(pessoaId) : BadRequest("Falha ao Cadastrar Pessoa");
    }
    
    /// <summary>
    ///     Endpoint para atualização de uma Pessoa
    /// </summary>
    /// <param name="pessoaViewModel">Dados da Pessoa</param>
    /// <returns>Boleano indicando sucesso da operação</returns>
    [HttpPut]
    [Route("Atualizar")]
    [ProducesResponseType(typeof(Ok<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BadRequest), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Put([FromBody] PessoaViewModel pessoaViewModel)
    {
        if (!ModelState.IsValid) return BadRequest();

        var contatoAtualizado = await _pessoaAppService.AtualizarPessoa(pessoaViewModel);

        return contatoAtualizado ? Ok(true) : BadRequest("Falha ao Atualizar Pessoa");
    }
    
    /// <summary>
    ///     Endpoint para remoção de uma pessoa por meio do identificador
    /// </summary>
    /// <param name="id">Identificador da Pessoa</param>
    /// <returns>Boleano indicando sucesso da operação</returns>
    [HttpDelete]
    [Route("Remover/{id:guid}")]
    [ProducesResponseType(typeof(Ok<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(BadRequest), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var contatoRemovido = await _pessoaAppService.RemoverPessoa(id);

        return contatoRemovido ? Ok(true) : BadRequest("Falha ao Remover Contato");
    }
}