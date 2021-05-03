using System;

namespace Exercise3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("enter lower bound");
            int A = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("enter upper bound");
            int B = Convert.ToInt32(Console.ReadLine());

            Solution s = new Solution();
            Console.WriteLine(s.GetPerfectSquare(A, B));
        }
    }
}
