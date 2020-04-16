using System;
using System.Collections.Generic;

namespace GradeBook
{
    class Program
    {
        static void Main(string[] args)
        {
            // var book = new InMemoryBook("Ebuka's Grade Book");
            IBook book = new DiskBook("Ebuka's Grade Book");

            // Event Handler
            book.GradeAdded += OnGradeAdded;

            EnterGrade(book);

            var stats = book.GetStatistics();


            Console.WriteLine($"For the book named {book.Name}");
            Console.WriteLine($"The lowest grade is {stats.Low}");
            Console.WriteLine($"The highest grade is {stats.High}");
            Console.WriteLine($"The average grade is {stats.Average:N1}");
            Console.WriteLine($"The letter grade is {stats.Letter}");
        }

        private static void EnterGrade(IBook book)
        {
            // Take in input of grades from user
            // var done = false;

            while (true)
            {
                Console.WriteLine("Enter a grade of 'q' to quit");
                var input = Console.ReadLine();

                if (input == "q")
                {
                    // done = true;
                    break;
                }

                try
                {
                    var grade = double.Parse(input);
                    book.AddGrade(grade);
                }
                catch (ArgumentException ex)
                {
                    // If greater than range
                    Console.WriteLine(ex.Message);
                }
                catch (FormatException ex)
                {
                    // If user enters rubbish
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    Console.WriteLine("Please put in a correct grade format!");
                }
            }
        }

        // Event Handler
        static void OnGradeAdded(object sender, EventArgs e)
        {
            Console.WriteLine("A grade was added");
        }
    }
}
