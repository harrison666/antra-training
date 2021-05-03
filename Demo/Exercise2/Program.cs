using System;

namespace Exercise2
{
    public class Arithmetic
    {
        int a = 2, b = 1;
        public void Addition()
        {
            Console.WriteLine(a + b);
        }
        public void Subtraction()
        {
            Console.WriteLine(a - b);
        }
        public void Multiplication()
        {
            Console.WriteLine(a * b);
        }
        public void Division()
        {
            Console.WriteLine(a / b);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please enter operation...");
            string operation = Console.ReadLine();
            Arithmetic a1 = new Arithmetic();
            switch (operation)
            {
                case "Addition":
                    a1.Addition();
                    break;
                case "Subtraction":
                    a1.Subtraction();
                    break;
                case "Multiplication":
                    a1.Multiplication();
                    break;
                case "Division":
                    a1.Division();
                    break;
            }
        }
    }
}
