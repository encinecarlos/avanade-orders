using orders.email.Domain.Entities;

namespace orders.email.Domain.Interfaces
{
    public interface IMessageServicehandler
    {
        Task SendMessage(Order order);
    }
}