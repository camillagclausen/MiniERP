using Microsoft.AspNetCore.Mvc;
using MiniERP.Services;
using MiniERP.Models;

namespace MiniERP.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly InventoryService _inventory;

    public ProductController(InventoryService inventory)
    {
        _inventory = inventory;
    }

    [HttpGet]
    public IActionResult GetProducts()
    {
        var products = _inventory.GetProducts();
        return Ok(products);
    }

    [HttpPost]
    public IActionResult AddProduct(Product product)
    {
        _inventory.AddProduct(product);
        return Ok("Product added");
    }
}