 using System;

namespace EmployeeReportApplication
{
    // Interface for any employee that can generate a report
    public interface IReportable
    {
        string GenerateReport();
    }

    // Developer class – reports on project progression
    public class Developer : IReportable
    {
        public string Name { get; set; }
        public int CompletionPercentage { get; set; }   // 0 to 100
        public string CurrentTask { get; set; }

        public Developer(string name, int completion, string task)
        {
            Name = name;
            CompletionPercentage = completion;
            CurrentTask = task;
        }

        public string GenerateReport()
        {
            return $"📊 Developer ({Name}) Report:\n" +
                   $"   - Project completion: {CompletionPercentage}%\n" +
                   $"   - Current task: {CurrentTask}\n";
        }
    }

    // Tester class – reports on bugs in the project
    public class Tester : IReportable
    {
        public string Name { get; set; }
        public int OpenBugs { get; set; }
        public int ClosedBugs { get; set; }
        public int CriticalBugs { get; set; }

        public Tester(string name, int open, int closed, int critical)
        {
            Name = name;
            OpenBugs = open;
            ClosedBugs = closed;
            CriticalBugs = critical;
        }

        public string GenerateReport()
        {
            return $"🐞 Tester ({Name}) Report:\n" +
                   $"   - Open bugs: {OpenBugs}\n" +
                   $"   - Closed bugs: {ClosedBugs}\n" +
                   $"   - Critical bugs: {CriticalBugs}\n";
        }
    }

    // Manager class – summarises both developer and tester reports
    public class Manager : IReportable
    {
        public string Name { get; set; }
        public Developer UnderDev { get; set; }
        public Tester UnderTester { get; set; }

        public Manager(string name, Developer dev, Tester tester)
        {
            Name = name;
            UnderDev = dev;
            UnderTester = tester;
        }

        public string GenerateReport()
        {
            string devReport = UnderDev.GenerateReport();
            string testerReport = UnderTester.GenerateReport();

            // Simple summary logic based on current data
            string summary;
            if (UnderDev.CompletionPercentage >= 80 && UnderTester.OpenBugs <= 5)
                summary = "✅ Project is on track! Good progress and low bug count.";
            else if (UnderDev.CompletionPercentage < 50)
                summary = "⚠️ Development is behind schedule. Urgent attention needed.";
            else if (UnderTester.CriticalBugs > 0)
                summary = "🔥 Critical bugs present! Fix them before next release.";
            else
                summary = "📌 Normal progress. Continue monitoring.";

            return $"👨‍💼 Manager ({Name}) Summary Report:\n" +
                   $"{devReport}\n" +
                   $"{testerReport}\n" +
                   $"📝 Overall assessment: {summary}\n";
        }
    }

    // Main application
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Manager's Employee Reporting System";
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("=== MANAGER REPORTING SYSTEM ===\n");
            Console.ResetColor();

            // Create employee instances
            Developer dev = new Developer("Alice", 65, "Implementing login module");
            Tester tester = new Tester("Bob", 8, 12, 2);
            Manager mgr = new Manager("Charlie", dev, tester);

            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("\n--- Main Menu ---");
                Console.WriteLine("1. View Developer Report");
                Console.WriteLine("2. View Tester Report");
                Console.WriteLine("3. View Manager Summary Report");
                Console.WriteLine("4. Update Developer Progress");
                Console.WriteLine("5. Update Tester Bugs");
                Console.WriteLine("6. Exit");
                Console.Write("Your choice: ");
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        Console.Clear();
                        Console.WriteLine(dev.GenerateReport());
                        break;
                    case "2":
                        Console.Clear();
                        Console.WriteLine(tester.GenerateReport());
                        break;
                    case "3":
                        Console.Clear();
                        Console.WriteLine(mgr.GenerateReport());
                        break;
                    case "4":
                        UpdateDeveloper(dev);
                        break;
                    case "5":
                        UpdateTester(tester);
                        break;
                    case "6":
                        exit = true;
                        Console.WriteLine("Exiting application...");
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        static void UpdateDeveloper(Developer dev)
        {
            Console.Clear();
            Console.WriteLine($"Current developer: {dev.Name}");
            Console.WriteLine($"Current completion: {dev.CompletionPercentage}%");
            Console.Write("Enter new completion percentage (0-100): ");
            if (int.TryParse(Console.ReadLine(), out int newProgress) && newProgress >= 0 && newProgress <= 100)
            {
                dev.CompletionPercentage = newProgress;
                Console.Write("Enter current task description: ");
                dev.CurrentTask = Console.ReadLine();
                Console.WriteLine("Developer information updated successfully.");
            }
            else
            {
                Console.WriteLine("Invalid percentage. Update cancelled.");
            }
        }

        static void UpdateTester(Tester tester)
        {
            Console.Clear();
            Console.WriteLine($"Current tester: {tester.Name}");
            Console.WriteLine($"Open bugs: {tester.OpenBugs}, Closed: {tester.ClosedBugs}, Critical: {tester.CriticalBugs}");
            Console.Write("Enter new number of open bugs: ");
            if (int.TryParse(Console.ReadLine(), out int open))
                tester.OpenBugs = open;
            Console.Write("Enter new number of closed bugs: ");
            if (int.TryParse(Console.ReadLine(), out int closed))
                tester.ClosedBugs = closed;
            Console.Write("Enter new number of critical bugs: ");
            if (int.TryParse(Console.ReadLine(), out int critical))
                tester.CriticalBugs = critical;
            Console.WriteLine("Tester information updated successfully.");
        }
    }
}
using System;

class Program
{
    static void Main()
    {
        // Default rounding uses MidpointRounding.ToEven (banker's rounding)
        Console.WriteLine(Math.Round(6.5));   // Nearest even integer → 6
        Console.WriteLine(Math.Round(11.5));  // Nearest even integer → 12
    }
}

using System;

class Program
{
    static void Main()
    {
        // 'A' has Unicode value 65
        Console.WriteLine(1 + 2 + 'A');   // (1+2)=3, 3+65=68
        Console.WriteLine(1 + 'A' + 2);   // 1+65=66, 66+2=68
        Console.WriteLine('A' + 1 + 2);   // 65+1=66, 66+2=68
    }
}
using System;

class Program
{
    static string str;        // reference type → null
    static DateTime time;     // value type → DateTime.MinValue

    static void Main()
    {
        // str is null → prints "str == null"
        Console.WriteLine(str == null ? "str == null" : str);

        // time is not null (DateTime cannot be null), so ToString() is called
        // DateTime.MinValue = 1/1/0001 12:00:00 AM (culture may affect format)
        Console.WriteLine(time == null ? "time == null" : time.ToString());
    }
}
using System;

public class TestStatic
{
    public static int TestValue;

    // Instance constructor
    public TestStatic()
    {
        if (TestValue == 0)
        {
            TestValue = 5;
        }
    }

    // Static constructor – runs before any instance is created
    static TestStatic()
    {
        if (TestValue == 0)
        {
            TestValue = 10;
        }
    }

    public void Print()
    {
        if (TestValue == 5)
        {
            TestValue = 6;
        }
        Console.WriteLine("TestValue : " + TestValue);
    }
}

class Program
{
    static void Main()
    {
        TestStatic t = new TestStatic(); // static ctor runs first (TestValue=10),
                                         // instance ctor sees 10 (not 0), so does nothing
        t.Print();                       // TestValue is still 10 → prints 10
    }
}
