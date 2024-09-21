using System.ComponentModel.DataAnnotations;
using FluentValidation;
using Person.Core.Domain.Entities.Enums;

namespace Person.Core.Domain.Entities;

public class Pessoa : EntidadeBase<Pessoa>
{
    [MaxLength(100)]
    public string Nome { get; }
    
    [MaxLength(100)]
    public string Email { get; }
    public TipoPessoa Tipo { get; }
    
    [MaxLength(15)]
    public string Documento { get; }
    public bool Status { get; private set; }
    
    public void AlterarStatus(bool status) => Status = status;

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
    
    public Pessoa(){ }

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