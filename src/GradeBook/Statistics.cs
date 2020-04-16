using System;

namespace GradeBook
{
    public class Statistics
    {
        // Fields of type double whose names are Average, High, Low
        public double Average
        {
            //  You cannot set average anymore
            // Readonly property

            get
            {
                return Sum / Count;
            }
        }
        public double High;
        public double Low;
        public char Letter
        {
            get
            {
                switch (Average)
                {
                    case var d when d >= 90.0:
                        return 'A';

                    case var d when d >= 80.0:
                        return 'B';

                    case var d when d >= 70.0:
                        return 'C';

                    case var d when d >= 60.0:
                        return 'D';

                    default:
                        return 'F';
                }
            }
        }

        public double Sum;

        public int Count;

        // Add number into statistics
        public void Add(double number)
        {
            Sum += number;
            Count += 1;

            Low = Math.Min(number, Low);
            High = Math.Max(number, High);
        }


        // Constructor
        public Statistics()
        {
            Count = 0;
            Sum = 0.0;
            High = double.MinValue;
            Low = double.MaxValue;
        }
    }
}