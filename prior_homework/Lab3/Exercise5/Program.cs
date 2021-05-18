using System;

namespace Exercise5
{
    class ComplexNumber
    {
        int a;
        int b;
        public ComplexNumber(int x, int y)
        {
            a = x;
            b = y;
        }
        public override string ToString()
        {
            if (b > 0)
            {
                return $"{a} + {b}i";
            }
            else if (b == 0)
            {
                return $"{a}";
            } 
            return $"{a} - {-b}i";

        }

        public void SetImaginary(int y)
        {
            b = y;
        }

        public double GetMagnitude()
        {
            return Math.Sqrt(a * a + b * b);
        }

        public void Add(ComplexNumber num)
        {
            a += num.a;
            b += num.b;
        }
    }

    class ComplexTest
    {
        static void Main(string[] args)
        {
            bool debug = false;

            ComplexNumber number = new ComplexNumber(5, 2);
            Console.WriteLine("Number is: " + number.ToString());

            number.SetImaginary(-3);
            Console.WriteLine("Number is: " + number.ToString());

            Console.Write("Magnitude is: ");
            Console.WriteLine(number.GetMagnitude());

            ComplexNumber number2 = new ComplexNumber(-1, 1);
            number.Add(number2);
            Console.Write("After adding: ");
            Console.WriteLine(number.ToString());

            if (debug)
                Console.ReadLine();
            Console.ReadKey();

        }
    }
}
