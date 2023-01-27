using orders.email.Domain.Entities;
using orders.email.Domain.Interfaces;

namespace orders.email.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private IMongodbServiceHandler<Order, string> Repository { get; }

        public OrderRepository(IMongodbServiceHandler<Order, string> repository)
        {
            Repository = repository;
        }

        public async Task<Order> getOrderByOrderId(string orderId)
        {
            return await Repository.GetSingleAsync(x => x.OrderId == orderId);
        }
    }
}