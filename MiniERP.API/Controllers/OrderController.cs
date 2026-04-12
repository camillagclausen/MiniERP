using Microsoft.AspNetCore.Mvc;
using MiniERP.Services;
using MiniERP.API.DTOs;
using MiniERP.DTOs;

namespace MiniERP.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly InventoryService _inventory;

    public OrderController(InventoryService inventory)
    {
        _inventory = inventory;
    }

    [HttpPost]
    public IActionResult CreateOrder([FromBody] CreateOrderRequest request)
    {
        var result = _inventory.CreateFullOrder(request.CustomerId, request.Items);

        if (!result.Success)
            return BadRequest(result.Message);

        return Ok(result.Receipt);
    }
}