using MiniERP.Models;
using MiniERP.Data;

namespace MiniERP.Services;

public class InventoryService
{
    private AppDBContext context = new AppDBContext();

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
        var existing = context.Customers
            .FirstOrDefault(c => c.PhoneNumber == customer.PhoneNumber);

        if (existing != null)
        {
            Console.WriteLine("Customer with this phone number already exists.");
            return;
        }

        context.Customers.Add(customer);
        context.SaveChanges();

        Console.WriteLine("Customer added!");
    }

    public void CreateFullOrder(string phoneNumber, List<(int productId, int quantity)> items)
    {
        var customer = context.Customers.FirstOrDefault(c => c.PhoneNumber == phoneNumber);

        if (customer == null)
        {
            Console.WriteLine("Customer not found");
            return;
        }

        var order = new Order
        {
            CustomerId = phoneNumber,
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

    public void ShowOrders()
    {
        var orders = context.Orders
            .Select(o => new
            {
                o.Id,
                CustomerName = o.Customer!.Name,
                Lines = o.OrderLines.Select(ol => new
                {
                    ProductName = ol.Product!.Name,
                    Price = ol.Product!.Price,
                    ol.Quantity,
                }).ToList()
            })
            .ToList();

        if (orders.Count == 0)
        {
            Console.WriteLine("No orders found.");
            return;
        }

        Console.WriteLine("\n--- Orders ---");

        foreach (var order in orders)
        {
            Console.WriteLine($"\nOrder #{order.Id}");
            Console.WriteLine($"Customer: {order.CustomerName}");

            decimal total = 0;

            foreach (var line in order.Lines)
            {
                var lineTotal = line.Price * line.Quantity;
                total += lineTotal;

                Console.WriteLine($"- {line.ProductName} x{line.Quantity} = {lineTotal}");
            }

            Console.WriteLine($"Total: {total}");
        }
    }

    public void ShowCustomers()
    {
        var customers = context.Customers.ToList();

        if (customers.Count == 0)
        {
            Console.WriteLine("No customers found.");
            return;
        }

        Console.WriteLine("\n--- Customers ---");

        foreach (var customer in customers)
        {
            Console.WriteLine($"Phone: {customer.PhoneNumber} | Name: {customer.Name}");
        }
    }

    public void RebootDatabase()
    {
        // Ryd database
        context.OrderLines.RemoveRange(context.OrderLines);
        context.Orders.RemoveRange(context.Orders);
        context.Products.RemoveRange(context.Products);
        context.Customers.RemoveRange(context.Customers);
        context.SaveChanges();

        // Tilføj produkter
        context.Products.AddRange(
            new Product { Name = "Blue Poster", Price = 5.99m, Stock = 1200 },
            new Product { Name = "Red Poster", Price = 7.99m, Stock = 1000 },
            new Product { Name = "Green Poster", Price = 3.99m, Stock = 900 }
        );

        // Tilføj kunde
        context.Customers.Add(new Customer
        {
            PhoneNumber = "42745004",
            Name = "Camilla Grubak"
        });

        context.SaveChanges();

        Console.WriteLine("Database rebooted with demo data!");
    }
}