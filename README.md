# MiniERP System

A simple ERP-style console application built in C# using .NET and Entity Framework Core.

## Features

- Add and manage products
- Manage customers using phone number as unique ID
- Create orders with multiple products
- Automatic stock updates
- Order receipt with total calculation
- View customers and order history
- Reset database with demo data

## Technologies

- C#
- .NET
- Entity Framework Core
- SQLite

## How to run

1. Clone the repository
2. Navigate to the project folder
3. Run:

dotnet ef database update
dotnet run

## Demo data

You can quickly load demo data using:

Menu → "Reboot database"

Includes:
- 3 products (posters)
- 1 customer

## Project structure

- Models/ → Data models (Product, Customer, Order, OrderLine)
- Services/ → Business logic
- Data/ → Database context
- Program.cs → Console UI

## Key design choices

- Customer uses phone number as primary key
- Separation of concerns (models, services, data)
- Database handled via Entity Framework migrations

## Future improvements

- Input validation (phone number format)
- Edit/delete functionality
- REST API
- GUI (web or desktop)
