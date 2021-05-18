using System;

namespace Exercise7
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter Your Pin Number");
            int pin = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("********Welcome to ATM Service**************");
            Console.WriteLine();
            Console.WriteLine("1. Check Balance");
            Console.WriteLine();
            Console.WriteLine("2. Withdraw Cash");
            Console.WriteLine();
            Console.WriteLine("3. Deposit Cash");
            Console.WriteLine();
            Console.WriteLine("4. Quit");
            Console.WriteLine("*********************************************");
            Console.WriteLine("Enter your choice:");
            int choice = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine(" YOU’RE BALANCE IN Rs: 1000");
        }
    }
}
