using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace orders.email.Domain.Interfaces
{
    public interface IMongodbServiceHandler<T, TId>
    {
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> predicate);
        Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate);
        Task InsertAsync(T entity);
        Task<ReplaceOneResult> UpdateAsync(TId id, T entity);
        Task RemoveAsync(TId id);
    }
}
