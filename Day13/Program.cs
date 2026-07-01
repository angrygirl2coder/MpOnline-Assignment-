using System;
using System.Collections.Generic;
using System.Linq;

namespace AgeBasedSorting
{
    // Simple Person class with Name and Age properties
    class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }

        // For easy display of person details
        public override string ToString()
        {
            return $"{Name} - {Age} years old";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Age Based Sorting of Persons ===\n");

            // Ask user how many persons they want to enter
            Console.Write("How many persons do you want to enter? ");
            int count = int.Parse(Console.ReadLine());

            // Create a list to store the persons
            List<Person> persons = new List<Person>();

            // Read person details from user
            for (int i = 0; i < count; i++)
            {
                Console.WriteLine($"\nEnter details for person #{i + 1}:");
                Console.Write("Name: ");
                string name = Console.ReadLine();

                Console.Write("Age: ");
                int age = int.Parse(Console.ReadLine());

                persons.Add(new Person { Name = name, Age = age });
            }

            // Display the entered list (unsorted)
            Console.WriteLine("\n--- Original List (unsorted) ---");
            DisplayPersons(persons);

            // Ask user for sorting order
            Console.WriteLine("\nSort by age:");
            Console.WriteLine("1 - Ascending (youngest first)");
            Console.WriteLine("2 - Descending (oldest first)");
            Console.Write("Your choice: ");
            string choice = Console.ReadLine();

            // Perform sorting based on user choice
            List<Person> sortedList = new List<Person>();
            if (choice == "1")
            {
                sortedList = persons.OrderBy(p => p.Age).ToList();
                Console.WriteLine("\n--- Sorted by Age (Ascending) ---");
            }
            else if (choice == "2")
            {
                sortedList = persons.OrderByDescending(p => p.Age).ToList();
                Console.WriteLine("\n--- Sorted by Age (Descending) ---");
            }
            else
            {
                Console.WriteLine("Invalid choice. Showing original list.");
                sortedList = persons;
            }

            // Display the sorted list
            DisplayPersons(sortedList);

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }

        // Helper method to display a list of persons
        static void DisplayPersons(List<Person> people)
        {
            if (people.Count == 0)
            {
                Console.WriteLine("No persons to display.");
                return;
            }

            foreach (Person p in people)
            {
                Console.WriteLine(p);
            }
        }
    }
}
