using orders.email.Application.Notifications;
using orders.email.Domain.Interfaces;

namespace orders.email.Domain.Services
{
    public class ProcessOrderService : BackgroundService
    {
        private ILogger<ProcessOrderService> Logger { get; }
        private IOrderRepository OrderRepository { get; }
        private IEventhandlerService EventService { get; }
        private IMessageServicehandler EmailHandler { get; }

        public ProcessOrderService(ILogger<ProcessOrderService> logger, IEventhandlerService eventService, IOrderRepository orderRepository, IMessageServicehandler emailHandler)
        {
            Logger = logger;
            EventService = eventService;
            OrderRepository = orderRepository;
            EmailHandler = emailHandler;
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

                    Logger.LogInformation($"Order received from database: {orderResult.OrderId}");

                    await EmailHandler.SendMessage(orderResult);
                    Logger.LogInformation("Proccess completed!");
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, $"Unexpected error: {ex.Message}");
            }
        }
    }
}