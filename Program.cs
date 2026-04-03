using System.Collections.Generic;
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
    Console.WriteLine("4. Add customer");
    Console.WriteLine("5. Create full order");
    Console.WriteLine("6. Show customers");
    Console.WriteLine("7. Show orders");
    Console.WriteLine("8. Reboot database (demo data)");
    Console.WriteLine("0. Exit");
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
            Console.Write("Phone number: ");
            var phone = Console.ReadLine();

            Console.Write("Customer name: ");
            var customerName = Console.ReadLine();

            inventory.AddCustomer(new Customer
            {
                PhoneNumber = phone ?? "",
                Name = customerName ?? ""
            });

            break;

        case "5":
            Console.Write("Customer phone number: ");
            string phoneNumber = Console.ReadLine() ?? "";

            var items = new List<(int productId, int quantity)>();

            bool addingProducts = true;

            while (addingProducts)
            {
                Console.Write("Product ID: ");
                int orderProductId;

                while (!int.TryParse(Console.ReadLine(), out orderProductId))
                {
                    Console.Write("Invalid input. Enter product ID again: ");
                }

                Console.Write("Quantity: ");
                int orderQuantity;

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

            inventory.CreateFullOrder(phoneNumber, items);

            break;

        case "6":
            inventory.ShowCustomers();
            break;

        case "7":
            inventory.ShowOrders();
            break;

        case "8":
            inventory.RebootDatabase();
            break;

        default:
            Console.WriteLine("Invalid choice");
            break;
    }

    Console.WriteLine("\nPress any key to continue...");
    Console.ReadKey();
}