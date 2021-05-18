using System;

namespace Exercise4
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("enter first number:");
            int lower = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("enter second number:");
            int upper = Convert.ToInt32(Console.ReadLine());
            int i;
            for (i = lower; i < upper + 1; i++)
            {
                int order = Convert.ToString(i).Length;
                int sumPower = 0;
                int temp = i, digit;
                while (temp != 0)
                {
                    digit = temp % 10;
                    temp /= 10;
                    sumPower += Convert.ToInt32(Math.Pow(digit, order));
                    // Console.WriteLine(sumPower);
                }

                if (i == sumPower)
                {
                    Console.WriteLine(i);
                }

            }
        }
    }
}
