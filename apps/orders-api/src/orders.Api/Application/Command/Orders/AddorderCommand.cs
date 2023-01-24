using MediatR;
using orders.Api.Application.Dto;

namespace orders.Api.Application.Command.Orders
{
    public class AddorderCommand : IRequest<OrderDto>
    {
        public OrderRequest? Order { get; set; }
    }
}