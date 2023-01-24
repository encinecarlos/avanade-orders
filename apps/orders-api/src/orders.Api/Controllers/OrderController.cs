using System.Net.Mime;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using orders.Api.Application.Command.Orders;
using orders.Api.Application.Dto;

namespace Orders.Api.controllers;

[ApiController]
[Route("/api/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Consumes(MediaTypeNames.Application.Json)]
public class OrderController : ControllerBase
{
    private IMediator Mediator { get; }

    public OrderController(IMediator mediator)
    {
        Mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok();
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<OrderDto>> CreateNewOrder([FromBody] AddorderCommand request, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(request, cancellationToken);

        return Accepted(result);
    }

}