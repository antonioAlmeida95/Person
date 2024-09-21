using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Person.Adapter.Driven.Database.UnitOfWork;
using Person.Core.Domain.Adapters.Database.Repository;
using Person.Core.Domain.Entities;

namespace Person.Adapter.Driven.Database.Repository;

public class PessoaRepository(PessoaContext context) : IDisposable, IPessoaRepository,
    IAsyncDisposable
{
    public async Task<bool> AdicionarPessoaAsync(Pessoa pessoa)
    {
        await context.AddAsync(pessoa);
        return await Commit();
    }

    public async Task<bool> AtualizarPessoa(Pessoa pessoa)
    {
        context.Update(pessoa);
        return await Commit();
    }

    public async Task<Pessoa?> ObterPessoaPorFiltro(Expression<Func<Pessoa, bool>> predicate, bool track = false) =>
        await Query(predicate,
            track: track).FirstOrDefaultAsync();


    public void Dispose() => context.Dispose();
    
    public async ValueTask DisposeAsync() => await context.DisposeAsync();

    private IQueryable<TX> Query<TX>(Expression<Func<TX, bool>>? expression = null, 
        Func<IQueryable<TX>, IIncludableQueryable<TX, object>>? include = null, bool track = false,
        int? skip = null, int? take = null) where TX : class
    {
        var query = context.GetQuery<TX>(track);

        if (expression != null)
            query = query.Where(expression);
        
        if (include != null)
            query = include(query);

        if (skip.HasValue)
            query = query.Skip(skip.Value);

        if (take.HasValue)
            query = query.Take(take.Value);

        return query;
    }

    private async Task<bool> Commit()
    {
        try
        {
            var linhasAfetadas = await context.SaveChangesAsync();
            return linhasAfetadas > 0;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Falha: {e.Message}");
            return false;
        }
    }
}