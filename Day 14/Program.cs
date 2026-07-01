using System;
using System.Collections.Generic;
using System.Linq;

class Order
{
    public int OrderId { get; set; }
    public DateTime OrderDate { get; set; }
    public string Product { get; set; }
}

class Program
{
    static void Main()
    {
        List<Order> orders = new List<Order>
        {
            new Order { OrderId = 1, OrderDate = new DateTime(2023, 5, 10), Product = "Laptop" },
            new Order { OrderId = 2, OrderDate = new DateTime(2022, 8, 15), Product = "Mouse" },
            new Order { OrderId = 3, OrderDate = new DateTime(2024, 1, 20), Product = "Keyboard" },
            new Order { OrderId = 4, OrderDate = new DateTime(2021, 12, 5), Product = "Monitor" }
        };

        Console.WriteLine("Display orders: 1-Oldest to Newest 2-Newest to Oldest");
        string choice = Console.ReadLine();

        List<Order> sortedOrders;

        if (choice == "1")
        {
            sortedOrders = orders.OrderBy(o => o.OrderDate).ToList();
            Console.WriteLine("Orders from oldest to newest:");
        }
        else if (choice == "2")
        {
            sortedOrders = orders.OrderByDescending(o => o.OrderDate).ToList();
            Console.WriteLine("Orders from newest to oldest:");
        }
        else
        {
            Console.WriteLine("Invalid choice");
            return;
        }

        foreach (var o in sortedOrders)
            Console.WriteLine($"{o.OrderId} {o.Product} {o.OrderDate.ToShortDateString()}");
    }
}
