using Microsoft.AspNetCore.Mvc;

namespace Orders.Api.controllers;

[ApiController]
[Route("/api/[controller]")]
public class OrderController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok();
    } 
}