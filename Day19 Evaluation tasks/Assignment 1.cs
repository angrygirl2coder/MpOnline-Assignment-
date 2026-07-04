//Write a method that prints the default values of different C# types int, bool, string, DateTime

using System;

class Program
{
    static void Main()
    {
        PrintDefaultValues();
    }


    static void PrintDefaultValues()
    {
        int defaultInt = default(int);
        bool defaultBool = default(bool);
        string defaultString = default(string);
        DateTime defaultDateTime = default(DateTime);

        Console.WriteLine($"Default int value: {defaultInt}");
        Console.WriteLine($"Default bool value: {defaultBool}");
        Console.WriteLine($"Default string value: {(defaultString == null ? "null" : defaultString)}");
        Console.WriteLine($"Default DateTime value: {defaultDateTime}");
    }
}
