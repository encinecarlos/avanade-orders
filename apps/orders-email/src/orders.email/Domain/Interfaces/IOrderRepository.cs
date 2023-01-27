using orders.email.Domain.Entities;

namespace orders.email.Domain.Interfaces
{
    public interface IOrderRepository
    {
        Task<Order> getOrderByOrderId(string orderId);
    }
}