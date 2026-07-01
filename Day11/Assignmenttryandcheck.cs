//Wap to check whether you have applied or whether you have submitted your exam form before deadline or not handle this with with a custom define exception


using System;

namespace ExamFormChecker
{
    public class ExamFormException : Exception
    {
        public ExamFormException(string message) : base(message) { }
    }

    class Program
    {
        static void Main(string[] args)
        {
            DateTime deadline = new DateTime(2026, 6, 1);

            try
            {
                Console.Write("Have you applied for the exam form? (y/n): ");
                string appliedInput = Console.ReadLine().ToLower();
                if (appliedInput != "y")
                {
                    throw new ExamFormException("Error: You have not applied for the exam form.");
                }

                Console.Write("Enter your exam form submission date (dd-mm-yyyy): ");
                if (!DateTime.TryParseExact(Console.ReadLine(), "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime submissionDate))
                {
                    throw new ExamFormException("Error: Invalid date format entered. Please use dd-mm-yyyy.");
                }

                if (submissionDate > deadline)
                {
                    throw new ExamFormException($"Error: You submitted the exam form on {submissionDate:dd-MM-yyyy}, which is after the deadline ({deadline:dd-MM-yyyy}).");
                }

                Console.WriteLine("Success: You have applied and submitted the exam form before the deadline.");
            }
            catch (ExamFormException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}
