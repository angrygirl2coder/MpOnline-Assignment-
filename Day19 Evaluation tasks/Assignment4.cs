//Convert hours to days days = h / 24


using System;

class Program
{
    static void Main()
    {

        Console.Write("Enter hours: ");
        // Read the input as a string and convert it to an integer
        int hours = int.Parse(Console.ReadLine());


        int days = hours / 24;


        Console.WriteLine($"{hours} hours is {days} full day(s).");
        //For fractional days, we can use a double to get a more precise result
        double fractionalDays = hours / 24.0;
        Console.WriteLine($"{hours} hours is {fractionalDays} day(s).");
    }
}
