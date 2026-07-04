//WAP To show Nullable<int> behaves when you assign null, compare it with another nullable, and use the HasValue and GetValueOrDefault properties.


using System;

class Program
{
    static void Main()
    {

        int? firstNullable = null;
        int? secondNullable = 42;
        int? thirdNullable = null;


        Console.WriteLine("--- Assigning null ---");
        Console.WriteLine($"firstNullable has value? {firstNullable.HasValue}");
        Console.WriteLine($"firstNullable value (or default): {firstNullable.GetValueOrDefault()}");
        Console.WriteLine($"firstNullable value (or custom default): {firstNullable.GetValueOrDefault(-1)}");


        Console.WriteLine("\n--- Comparing nullable ints ---");
        bool areEqual1 = (firstNullable == secondNullable);
        Console.WriteLine($"firstNullable == secondNullable ? {areEqual1}");


        bool areEqual2 = (firstNullable == thirdNullable);
        Console.WriteLine($"firstNullable == thirdNullable ? {areEqual2}");


        Console.WriteLine("\n--- Using GetValueOrDefault() for comparison ---");
        bool valueEqual = (firstNullable.GetValueOrDefault() == secondNullable.GetValueOrDefault());
        Console.WriteLine($"firstNullable.GetValueOrDefault() == secondNullable.GetValueOrDefault() ? {valueEqual}");


        Console.WriteLine("\n--- Conditional use ---");
        if (secondNullable.HasValue)
        {
            Console.WriteLine($"secondNullable has value: {secondNullable.Value}");
        }
        else
        {
            Console.WriteLine("secondNullable is null");
        }


        int result = firstNullable.GetValueOrDefault(100);
        Console.WriteLine($"firstNullable.GetValueOrDefault(100) = {result}");
    }
}
