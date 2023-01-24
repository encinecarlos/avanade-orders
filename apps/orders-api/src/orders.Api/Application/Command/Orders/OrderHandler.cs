using AutoMapper;
using MediatR;
using orders.Api.Application.Command.Orders;
using orders.Api.Application.Dto;
using orders.Api.Application.Notifications;
using orders.Api.Domain.Entites;
using orders.Api.Domain.Interfaces;

namespace orders.Api.Application.Command.orders
{
    public class OrderHandler : IRequestHandler<AddorderCommand, OrderDto>
    {
        private readonly ILogger<OrderHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IOrderRepository _ordersRepository;
        private readonly IEventHandlerService _eventHandlerService;

        public OrderHandler(ILogger<OrderHandler> logger, IMapper mapper, IOrderRepository ordersRepository, IEventHandlerService eventHandlerService)
        {
            _logger = logger;
            _mapper = mapper;
            _ordersRepository = ordersRepository;
            _eventHandlerService = eventHandlerService;
        }

        public async Task<OrderDto> Handle(AddorderCommand request, CancellationToken cancellationToken)
        {
            var result = _mapper.Map<Order>(request.Order);

            _logger.LogInformation("Save order on database");
            await _ordersRepository.InsertOrderAsync(result);

            _logger.LogInformation("Produce message");
            var notification = new OrderNotification
            {
                OrderId = result.OrderId,
                OrderDate = result.OrderDate
            };

            await _eventHandlerService.produceMessage<OrderNotification>("send-email", notification);

            return await Task.FromResult(new OrderDto
            {
                Message = $"order nº {notification.OrderId} created at {result.OrderDate:dd/MM/yyyy}",
                orderId = result.OrderId
            });

        }
    }
}