using System;

public class Class1
{
	public Class1()
	{public void ifelse1()
    {
        // Ask the user to enter the first number
        Console.Write("Enter first number: ");
        int num1 = Convert.ToInt32(Console.ReadLine());

        // Ask the user to enter the second number
        Console.Write("Enter second number: ");
        int num2 = Convert.ToInt32(Console.ReadLine());

        // Ask the user to enter the third number
        Console.Write("Enter third number: ");
        int num3 = Convert.ToInt32(Console.ReadLine());

        // Check if the first number is greater than or equal to the other two numbers
        if (num1 >= num2 && num1 >= num3)
        {
            Console.WriteLine($"Greatest number is: {num1}");
        }

        // Check if the second number is greater than or equal to the other two numbers
        else if (num2 >= num1 && num2 >= num3)
        {
            Console.WriteLine($"Greatest number is: {num2}");
        }

        // Otherwise, the third number is the greatest
        else
        {
            Console.WriteLine($"Greatest number is: {num3}");
        }
    }
}
}
