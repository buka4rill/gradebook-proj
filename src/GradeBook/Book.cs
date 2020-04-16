using System;
using System.Collections.Generic;
using System.IO;

namespace GradeBook
{
    // Delegate definition
    public delegate void GradeAddedDelegate(object sender, EventArgs args);


    public class NamedObject
    {
        public NamedObject(string name)
        {
            Name = name;
        }

        public string Name
        {
            get;
            set;
        }
    }

    // Defining a new type with an Interface
    public interface IBook
    {
        // Pure abstraction
        void AddGrade(double grade);
        Statistics GetStatistics();
        string Name { get; }
        event GradeAddedDelegate GradeAdded;
    }

    // Some polymorphism so classes derived from class
    // cann have different implementations of AddGrade
    public abstract class Book : NamedObject, IBook
    {
        public Book(string name) : base(name)
        {
        }

        public abstract event GradeAddedDelegate GradeAdded;

        public abstract void AddGrade(double grade);

        public abstract Statistics GetStatistics();


    }

    public class DiskBook : Book
    {
        // Clas constructor
        public DiskBook(string name) : base(name)
        {

        }

        public override event GradeAddedDelegate GradeAdded;

        public override void AddGrade(double grade)
        {
            using (var writer = File.AppendText($"{Name}.txt"))
            {
                writer.WriteLine(grade);
                if (GradeAdded != null)
                {
                    GradeAdded(this, new EventArgs());
                }
            }
            // C-Sharp then automatically disposes the IDisposible object

            // When you open a file, you need to close the file when finished
            // writer.Dispose();
        }

        public override Statistics GetStatistics()
        {
            // Read all the grades placed into the file

            var result = new Statistics();

            using (var reader = File.OpenText($"{Name}.txt"))
            {
                var line = reader.ReadLine();

                while (line != null) // Else we've reached end of file
                {
                    var number = double.Parse(line);
                    result.Add(number);
                    line = reader.ReadLine();
                }
            }

            return result;
        }
    }

    public class InMemoryBook : Book
    {
        //  Book constructor (explicit)
        public InMemoryBook(string name) : base(name)
        {
            grades = new List<double>();
            Name = name;
        }

        public void AddGrade(char letter)
        {
            switch (letter)
            {
                case 'A':
                    AddGrade(90);
                    break;

                case 'B':
                    AddGrade(80);
                    break;

                case 'C':
                    AddGrade(70);
                    break;

                default:
                    AddGrade(0);
                    break;
            }
        }

        public override void AddGrade(double grade)
        {
            if (grade <= 100 && grade >= 0)
            {
                grades.Add(grade);

                if (GradeAdded != null)
                {
                    GradeAdded(this, new EventArgs());
                }
            }
            else
            {
                throw new ArgumentException($"Invalid {nameof(grade)}");
            }
        }

        public override event GradeAddedDelegate GradeAdded;

        public override Statistics GetStatistics()
        {
            // New instance of the object Statistics
            var result = new Statistics();

            for (var index = 0; index < grades.Count; index++)
            {
                result.Add(grades[index]);
            }

            // Return statistics
            return result;
        }

        // Private fields
        private List<double> grades;

        // Setting Property
        // public string Name
        // {
        //     get;
        //     set;
        // }

        // private string name; // Backing field for the prop.

        // Readonly field
        public const string CATEGORY = "Science";
    }
}