namespace MiniERP.DTOs;

public class OrderReceiptDto
{
    public string Customer { get; set; } = "";
    public List<OrderLineReceiptDto> Items { get; set; } = new();
    public decimal Total { get; set; }
}

public class OrderLineReceiptDto
{
    public string Product { get; set; } = "";
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal LineTotal { get; set; }
}