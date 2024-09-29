using Person.Core.Domain.Entities;
using Person.Core.Domain.Entities.Enums;

namespace Person.Adapters.Api.Test.AppService;

public static class PessoaFactory
{
    public static Pessoa CriarPessoa(string? nome = null, string? email = null,
        TipoPessoa? tipo = null, string? documento = null, Guid? id = null) =>
        new(nome ?? string.Empty, email ?? string.Empty, tipo ?? TipoPessoa.Fisica,
            documento ?? string.Empty, id);
}