# MiniERP System

A console-based ERP-style system built in C# using .NET and SQLite.

## Features

- Product management (add + view inventory)
- Customer management
- Create orders with multiple products
- Automatic stock updates
- Order receipts with total price
- Data persistence using SQLite database

## Technologies

- C#
- .NET
- Entity Framework Core
- SQLite

## How to run

1. Clone the repository
2. Navigate to the project folder
3. Run:

dotnet run

## Example flow

1. Add a customer
2. Add products
3. Create a full order
4. View inventory updates

## Project structure

- Models/ → Data models (Product, Customer, Order, OrderLine)
- Services/ → Business logic
- Data/ → Database context
- Program.cs → Main UI / menu

## Future improvements

- Show order history
- Invoice generation
- REST API
- UI (web or desktop)