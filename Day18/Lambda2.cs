using System;
using System.Collections.Generic;
using System.Linq;

public class Item
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
}

public class CartItem
{
    public Item Item { get; set; }
    public int Quantity { get; set; }
}

public class ShoppingCart
{
    public List<CartItem> Items { get; set; } = new List<CartItem>();

    public decimal GetTotal() => Items.Sum(ci => ci.Item.Price * ci.Quantity);

    public decimal ApplyDiscount(Func<decimal, decimal> discountStrategy)
        => discountStrategy(GetTotal());

    public List<CartItem> FilterByPrice(decimal minPrice)
        => Items.Where(ci => ci.Item.Price > minPrice).ToList();
}

class Program
{
    static void Main()
    {
        var items = new List<Item>
        {
            new Item { Id = 1, Name = "Laptop", Price = 1200m },
            new Item { Id = 2, Name = "Mouse", Price = 25m },
            new Item { Id = 3, Name = "Keyboard", Price = 75m },
            new Item { Id = 4, Name = "Monitor", Price = 300m }
        };

        var cart = new ShoppingCart();
        cart.Items.Add(new CartItem { Item = items[0], Quantity = 1 });
        cart.Items.Add(new CartItem { Item = items[1], Quantity = 2 });
        cart.Items.Add(new CartItem { Item = items[2], Quantity = 1 });
        cart.Items.Add(new CartItem { Item = items[3], Quantity = 1 });

        decimal total = cart.GetTotal();
        Console.WriteLine($"Total: {total:C}");

        Func<decimal, decimal> tenPercentOff = t => t * 0.9m;
        decimal discounted = cart.ApplyDiscount(tenPercentOff);
        Console.WriteLine($"After 10% discount: {discounted:C}");

        decimal filterPrice = 100m;
        var expensiveItems = cart.FilterByPrice(filterPrice);
        Console.WriteLine($"Items with price > {filterPrice:C}:");
        foreach (var ci in expensiveItems)
            Console.WriteLine($"  {ci.Item.Name} - {ci.Item.Price:C} x {ci.Quantity}");
    }
}
