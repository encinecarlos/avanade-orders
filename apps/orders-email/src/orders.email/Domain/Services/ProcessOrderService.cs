using orders.email.Application.Notifications;
using orders.email.Domain.Interfaces;

namespace orders.email.Domain.Services
{
    public class ProcessOrderService : BackgroundService
    {
        private ILogger<ProcessOrderService> Logger { get; }
        private IOrderRepository OrderRepository { get; }
        private IEventhandlerService EventService { get; }

        public ProcessOrderService(ILogger<ProcessOrderService> logger, IEventhandlerService eventService, IOrderRepository orderRepository)
        {
            Logger = logger;
            EventService = eventService;
            OrderRepository = orderRepository;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Logger.LogInformation($"Start proccess...");

            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    var message = await EventService.ConsumeMessage<OrderNotification>(stoppingToken);

                    Logger.LogInformation($"OrderId received: {message.OrderId}");

                    var orderResult = await OrderRepository.getOrderByOrderId(message.OrderId);

                    Logger.LogInformation($"Message received: {orderResult.OrderId}");
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"Unexpected error: {ex.Message}");
            }
        }
    }
}