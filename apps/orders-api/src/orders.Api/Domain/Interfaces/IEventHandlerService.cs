namespace orders.Api.Domain.Interfaces
{
    public interface IEventHandlerService
    {
        Task<bool> produceMessage<T>(string topic, T content);
    }
}