using System.Diagnostics.CodeAnalysis;
using FluentValidation;
using Person.Core.Domain.Entities.Enums;

namespace Person.Core.Domain.Entities;

[ExcludeFromCodeCoverage]
public class Pessoa : EntidadeBase<Pessoa>
{
    public string Nome { get; }
    public string Email { get; }
    public TipoPessoa Tipo { get; }
    public string Documento { get; }
    public bool Status { get; private set; }
    
    public void AlterarStatus(bool status) => Status = status;
    public Pessoa(){ }
    
    public Pessoa(string nome, string email, TipoPessoa tipo, string documento)
        : this(nome, email, tipo, documento, null) { }
    public Pessoa(string nome, string email, TipoPessoa tipo, string documento, Guid? id = null) 
    {
        Id = id ?? Guid.NewGuid();
        Nome = nome;
        Email = email;
        Tipo = tipo;
        Documento = documento;
    }
    
    public override bool ValidarEntidade()
    {
        RuleFor(x => x.Nome)
            .NotEmpty()
            .WithMessage("O Nome é um campo obrigatório");

        RuleFor(x => x.Documento)
            .NotEmpty()
            .WithMessage("Documento é um campo obrigatório");
        
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("O Email é obrigatório");

        RuleFor(x => x.Email)
            .EmailAddress()
            .WithMessage("O Email está inválido");
        
        ValidationResult = Validate(this);
        return ValidationResult.IsValid;
    }
}