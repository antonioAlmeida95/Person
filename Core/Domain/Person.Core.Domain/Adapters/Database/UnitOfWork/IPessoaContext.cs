using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Person.Core.Domain.Entities;

namespace Person.Core.Domain.Adapters.Database.UnitOfWork;

public interface IPessoaContext
{
    DbSet<Pessoa> Pessoa { get; }
    DatabaseFacade GetDatabase();
    
    /// <summary>
    ///     Método para Obtenção da Query setando a tabela do contexto
    /// </summary>
    /// <param name="track">Trackeamento da entidade</param>
    /// <typeparam name="T">Classe Base</typeparam>
    /// <returns>Query</returns>
    IQueryable<T> GetQuery<T>(bool track) where T: class;
    
    /// <summary>
    ///     Método para Obtenção da Query setando a tabela do contexto
    /// </summary>
    /// <typeparam name="T">Classe Base</typeparam>
    /// <returns>Query</returns>
    IQueryable<T> GetQuery<T>() where T: class;
}