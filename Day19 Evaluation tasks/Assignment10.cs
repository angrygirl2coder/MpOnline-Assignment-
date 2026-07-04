//WAP to print groups of anagrams from a given list from 1to 50


using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {

        List<string> numbers = new List<string>();
        for (int i = 1; i <= 50; i++)
            numbers.Add(i.ToString());

        Console.WriteLine("Anagram groups using numbers 1–50:\n");
        PrintAnagramGroups(numbers);
    }

    static void PrintAnagramGroups(List<string> items)
    {

        var groups = items.GroupBy(str =>
        {
            char[] chars = str.ToCharArray();
            Array.Sort(chars);
            return new string(chars);
        });

        int groupNo = 1;
        foreach (var group in groups)
        {
            Console.WriteLine($"Group {groupNo}: " + string.Join(", ", group));
            groupNo++;
        }
    }
}
