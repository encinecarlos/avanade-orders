using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Driver;
using orders.Api.Domain.Entites;

namespace orders.Api.Domain.Interfaces
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> getAllOrdersAsync();
        Task<Order> GetSingleOrderAsync(string id);
        Task InsertOrderAsync(Order entity);
        Task<ReplaceOneResult> UpdateOrderAsync(string id, Order entity);
        Task RemoveOrderAsync(string id);
    }
}