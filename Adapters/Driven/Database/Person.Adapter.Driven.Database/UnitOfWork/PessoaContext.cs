using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Person.Adapter.Driven.Database.Mappings;
using Person.Core.Domain.Adapters.Database.UnitOfWork;
using Person.Core.Domain.Entities;

namespace Person.Adapter.Driven.Database.UnitOfWork;

[ExcludeFromCodeCoverage]
public partial class PessoaContext: DbContext, IPessoaContext
{
    
    public DbSet<Pessoa> Pessoa { get; set; }
    
    private string? _connectionString;
    
    public PessoaContext(string? connectionString = null) => _connectionString = connectionString;

    public DatabaseFacade GetDatabase() => Database;
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new PessoaMapping());
    }
}