using MiniERP.Models;
using MiniERP.Data;

namespace MiniERP.Services;

public class InventoryService
{
    private AppDBContext context = new AppDBContext();
    private int nextId = 1;

    public void AddProduct(Product product)
    {
        context.Products.Add(product);
        context.SaveChanges();
    }

    public void ShowInventory()
    {
        Console.WriteLine("\n--- Inventory ---");

        var products = context.Products.ToList();

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

    public void CreateOrder(int productId, int quantity)
    {
        var product = context.Products.FirstOrDefault(p => p.Id == productId);

        if (product == null)
        {
            Console.WriteLine("Product not found");
            return;
        }

        if (product.Stock < quantity)
        {
            Console.WriteLine("Not enough stock");
            return;
        }

        product.Stock -= quantity;
        context.SaveChanges();

        Console.WriteLine($"Order created! Remaining stock: {product.Stock}");
    }

    public void AddCustomer(Customer customer)
    {
        context.Customers.Add(customer);
        context.SaveChanges();
    }

    public void CreateFullOrder(int customerId, List<(int productId, int quantity)> items)
{
    var customer = context.Customers.FirstOrDefault(c => c.Id == customerId);

    if (customer == null)
    {
        Console.WriteLine("Customer not found");
        return;
    }

    var order = new Order
    {
        CustomerId = customerId,
        OrderLines = new List<OrderLine>()
    };

    decimal total = 0;

    Console.WriteLine("\n--- Order Receipt ---");
    Console.WriteLine($"Customer: {customer.Name}");
    Console.WriteLine("\nProducts:");

    foreach (var item in items)
    {
        var product = context.Products.FirstOrDefault(p => p.Id == item.productId);

        if (product == null)
        {
            Console.WriteLine($"Product {item.productId} not found");
            continue;
        }

        if (product.Stock < item.quantity)
        {
            Console.WriteLine($"Not enough stock for {product.Name}");
            continue;
        }

        product.Stock -= item.quantity;

        var lineTotal = product.Price * item.quantity;
        total += lineTotal;

        Console.WriteLine($"- {product.Name} x{item.quantity} = {lineTotal}");

        order.OrderLines.Add(new OrderLine
        {
            ProductId = product.Id,
            Quantity = item.quantity
        });
    }

    context.Orders.Add(order);
    context.SaveChanges();

    Console.WriteLine("\n----------------------");
    Console.WriteLine($"Total: {total}");
    Console.WriteLine("----------------------");
    Console.WriteLine("Order created successfully!");
}
}