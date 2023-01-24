using System.Linq.Expressions;
using MongoDB.Driver;

namespace Orders.Api.Domain.Interfaces;

public interface IMongoDbServiceHandler<T, TId>
{
    Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate);
    Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate);
    Task InsertAsync(T entity);
    Task<ReplaceOneResult> UpdateAsync(TId id, T entity);
    Task RemoveAsync(TId id);
}