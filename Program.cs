using MiniERP.Models;
using MiniERP.Services;

var inventory = new InventoryService();

bool running = true;

while (running)
{
    Console.Clear();
    Console.WriteLine("=== Mini ERP System ===");
    Console.WriteLine("1. Add product");
    Console.WriteLine("2. Show inventory");
    Console.WriteLine("3. Create order");
    Console.WriteLine("4. Exit");
    Console.WriteLine("------------------------");
    Console.Write("Choose an option: ");

    var input = Console.ReadLine();

    switch (input)
    {
        case "1":
            Console.Write("Product name: ");
            var name = Console.ReadLine();

            Console.Write("Price: ");
            decimal price;

            while (!decimal.TryParse(Console.ReadLine(), out price))
            {
                Console.Write("Invalid input. Enter price again: ");
            }

            int stock;
            Console.Write("Stock: ");

            while (!int.TryParse(Console.ReadLine(), out stock))
            {
                Console.Write("Invalid input. Enter stock again: ");
            }

            inventory.AddProduct(new Product
            {
                Name = name ?? "",
                Price = price,
                Stock = stock
            });

            Console.WriteLine("Product added!");
            break;

        case "2":
            inventory.ShowInventory();
            break;

        case "3":
            Console.Write("Product ID: ");
            int productId = int.Parse(Console.ReadLine() ?? "0");

            Console.Write("Quantity: ");
            int quantity = int.Parse(Console.ReadLine() ?? "0");

            inventory.CreateOrder(new Order
            {
                ProductId = productId,
                Quantity = quantity
            });
            break;

        case "4":
            running = false;
            break;

        default:
            Console.WriteLine("Invalid choice");
            break;
    }

    Console.WriteLine("\nPress any key to continue...");
    Console.ReadKey();
}