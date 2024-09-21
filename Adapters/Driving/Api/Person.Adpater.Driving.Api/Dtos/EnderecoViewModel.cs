using System.ComponentModel.DataAnnotations;

namespace Person.Adpater.Driving.Api.Dtos;

public class EnderecoViewModel
{
    public Guid Id { get; set; }
    [MaxLength(100)]
    public string? Logradouro { get; set; }
    public long Cep { get; set; }
    [MaxLength(100)]
    public string? Bairro { get; set; }
    [MaxLength(100)]
    public string? Cidade { get; set; }
    [MaxLength(3)]
    public string? Uf { get; set; }
    public long Numero { get; set; }
}