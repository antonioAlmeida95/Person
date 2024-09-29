using System.ComponentModel.DataAnnotations;
using Person.Adpater.Driving.Api.Dtos.Enum;

namespace Person.Adpater.Driving.Api.Dtos;

public class PessoaViewModel
{
    public Guid PessoaId { get; set; }
    
    [MaxLength(100)]
    public string? Nome { get; set; }
    
    [MaxLength(100)]
    public string? Email { get; set; }
    
    public TipoPessoaViewModel Tipo { get; set; }
    
    [MaxLength(15)]
    public string? Documento { get; set; }
}