using System;

namespace Exercise3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Write the string you want to reverse");
            string s = Console.ReadLine();
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            Console.WriteLine(new string(charArray));
        }
    }
}
