using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Person.Core.Domain.Entities;

namespace Person.Adapter.Driven.Database.Mappings;

[ExcludeFromCodeCoverage]
public class PessoaMapping : IEntityTypeConfiguration<Pessoa>
{
    public void Configure(EntityTypeBuilder<Pessoa> builder)
    {
        builder.Property(x => x.Id)
            .HasColumnName("Pes_PessoaId");
        
        builder.Property(x => x.Nome)
            .HasColumnName("Pes_Nome");
        
        builder.Property(x => x.Email)
            .HasColumnName("Pes_Email");
        
        builder.Property(x => x.Tipo)
            .HasColumnName("Pes_Tipo");
        
        builder.Property(x => x.Documento)
            .HasColumnName("Pes_Documento");
        
        builder.Property(x => x.Status)
            .HasColumnName("Pes_Status")
            .HasDefaultValue(true);
        
        builder.Ignore(x => x.ValidationResult);
        builder.Ignore(x => x.ClassLevelCascadeMode);
        builder.Ignore(x => x.RuleLevelCascadeMode);
        builder.Ignore(x => x.CascadeMode);
        
        builder.ToTable("Pes_Pessoa", "Cadastro");
    }
}