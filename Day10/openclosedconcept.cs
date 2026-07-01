// solid principles, ocp 
using System;

public abstract class Discount
{
    public abstract double ApplyDiscount(double price);
}

public class PercentageDiscount : Discount
{
    private readonly double _percentage;

    public PercentageDiscount(double percentage)
    {
        _percentage = percentage;
    }

    public override double ApplyDiscount(double price)
    {
        return price - (price * _percentage / 100);
    }
}

public class FixedDiscount : Discount
{
    private readonly double _amount;

    public FixedDiscount(double amount)
    {
        _amount = amount;
    }

    public override double ApplyDiscount(double price)
    {
        return price - _amount;
    }
}

internal class Open_Close_Example
{
    public static void Application()
    {
        Discount discount = new PercentageDiscount(10);
        Console.WriteLine(discount.ApplyDiscount(200)); // Output: 180

        discount = new FixedDiscount(30);
        Console.WriteLine(discount.ApplyDiscount(200)); // Output: 170
    }
}

// Entry point for the application
public class Program
{
    public static void Main()
    {
        Open_Close_Example.Application();
    }
}
public class TestStatic
{
    public static int TestValue;

    public TestStatic() { if (TestValue == 0) { TestValue = 5; } }
    static TestStatic() { if (TestValue == 0) { TestValue = 10; } }
    public void Print()
    {
        if (TestValue == 5) { TestValue = 6; }
        Console.WriteLine("TestValue : " + TestValue);
    }
}
public void Main(string[] args)
{
    TestStatic t = new TestStatic();
    t.Print();
}
