//print groups of anagrams from a given list



using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        List<string> words = new List<string>
        {
            "listen", "silent", "enlist",
            "hello", "olleh",
            "world", "dlrow",
            "apple", "papel",
            "cat", "dog"
        };

        Console.WriteLine("Anagram groups in the list:\n");
        PrintAnagramGroups(words);
    }


    static void PrintAnagramGroups(List<string> words)
    {

        var groups = words.GroupBy(word =>
        {

            char[] chars = word.ToCharArray();
            Array.Sort(chars);
            return new string(chars);
        });

        int groupNumber = 1;
        foreach (var group in groups)
        {

            Console.WriteLine($"Group {groupNumber}: " + string.Join(", ", group));
            groupNumber++;
        }
    }
}
