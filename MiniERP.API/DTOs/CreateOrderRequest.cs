using MiniERP.DTOs;

namespace MiniERP.API.DTOs;

public class CreateOrderRequest
{
    public string CustomerId { get; set; } = "";
    public List<OrderItemDto> Items { get; set; } = new();
}