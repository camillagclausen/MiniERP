using Microsoft.AspNetCore.Mvc;
using MiniERP.Services;
using MiniERP.Models;

namespace MiniERP.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomerController : ControllerBase
{
    private readonly InventoryService _inventory;

    public CustomerController(InventoryService inventory)
    {
        _inventory = inventory;
    }

    [HttpGet]
    public IActionResult GetCustomers()
    {
        var customers = _inventory.GetCustomers();
        return Ok(customers);
    }

    [HttpPost]
    public IActionResult AddCustomer([FromBody] Customer customer)
    {
        var result = _inventory.AddCustomer(customer);

        if (!result)
            return BadRequest("Customer already exists");

        return Ok(customer);
    }
}