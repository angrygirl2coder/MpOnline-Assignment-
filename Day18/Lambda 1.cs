using System;
using System.Collections.Generic;
using System.Linq;

public class Employee
{
    public string Name { get; set; }
    public decimal Salary { get; set; }

    public Employee(string name, decimal salary)
    {
        Name = name;
        Salary = salary;
    }

    public override string ToString()
    {
        return $"{Name} - {Salary:C}";
    }
}

class Program
{
    static void Main()
    {
        // 1. Create a list of employees
        List<Employee> employees = new List<Employee>
        {
            new Employee("Alice", 7500m),
            new Employee("Bob", 12000m),
            new Employee("Charlie", 9500m),
            new Employee("Diana", 15000m),
            new Employee("Eve", 5000m)
        };

        // 2. Use a lambda expression to filter employees earning > 10,000
        var highEarners = employees.Where(emp => emp.Salary > 10000m);

        // 3. Display the filtered results
        Console.WriteLine("Employees earning more than 10,000:");
        foreach (var emp in highEarners)
        {
            Console.WriteLine(emp);
        }
    }
}
