using MiniERP.Models;

namespace MiniERP.Services;

public class InventoryService
{
    private List<Product> products = new List<Product>();
    private int nextId = 1;

    public void AddProduct(Product product)
    {
        products.Add(product);
        product.Id = nextId++;
    }

    public void ShowInventory()
    {
        Console.WriteLine("\n--- Inventory ---");

        if (products.Count == 0)
        {
            Console.WriteLine("No products available.");
            return;
        }

        foreach (var product in products)
        {
            Console.WriteLine($"ID: {product.Id} | Name: {product.Name} | Stock: {product.Stock} | Price: {product.Price}");
        }
    }

    public void CreateOrder(Order order)
    {
        var product = products.FirstOrDefault(p => p.Id == order.ProductId);

        if (product == null)
        {
            Console.WriteLine("Product not found");
            return;
        }

        if (product.Stock < order.Quantity)
        {
            Console.WriteLine("Not enough stock");
            return;
        }

        product.Stock -= order.Quantity;

        Console.WriteLine($"Order created! Remaining stock: {product.Stock}");
    }
}