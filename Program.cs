using System.Collections.Generic;
using MiniERP.Models;
using MiniERP.Services;

var inventory = new InventoryService();

inventory.AddCustomer(new Customer { Name = "Test Customer" });

bool running = true;

while (running)
{
    Console.Clear();
    Console.WriteLine("=== Mini ERP System ===");
    Console.WriteLine("0. Exit");
    Console.WriteLine("1. Add product");
    Console.WriteLine("2. Show inventory");
    Console.WriteLine("3. Create order");
    Console.WriteLine("4. Add customer");
    Console.WriteLine("5. Create full order");
    Console.WriteLine("------------------------");
    Console.Write("Choose an option: ");

    var input = Console.ReadLine();

    switch (input)
    {
        case "0":
            running = false;
            break;
        
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

            inventory.CreateOrder(productId, quantity);
            break;

        case "4":
            Console.Write("Customer name: ");
            var customerName = Console.ReadLine();

            inventory.AddCustomer(new Customer
            {
                Name = customerName ?? ""
            });

            Console.WriteLine("Customer added!");
            break;

        case "5":
            Console.Write("Customer ID: ");
            int customerId;

            while (!int.TryParse(Console.ReadLine(), out customerId))
            {
                Console.Write("Invalid input. Enter customer ID again: ");
            }

            var items = new List<(int productId, int quantity)>();

            bool addingProducts = true;

            while (addingProducts)
            {
                Console.Write("Product ID: ");
                int orderProductId;   // 👈 ændret navn

                while (!int.TryParse(Console.ReadLine(), out orderProductId))
                {
                    Console.Write("Invalid input. Enter product ID again: ");
                }

                Console.Write("Quantity: ");
                int orderQuantity;    // 👈 ændret navn

                while (!int.TryParse(Console.ReadLine(), out orderQuantity))
                {
                    Console.Write("Invalid input. Enter quantity again: ");
                }

                items.Add((orderProductId, orderQuantity));

                Console.Write("Add another product? (y/n): ");
                var more = Console.ReadLine();

                if (more?.ToLower() != "y")
                {
                    addingProducts = false;
                }
            }

            inventory.CreateFullOrder(customerId, items);

            break;

        default:
            Console.WriteLine("Invalid choice");
            break;
    }

    Console.WriteLine("\nPress any key to continue...");
    Console.ReadKey();
}