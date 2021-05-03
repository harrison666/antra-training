using System;

namespace Exercise2
{
    class Program
    {
        static void Main(string[] args)
        {
            Solution s = new Solution();
            Console.WriteLine("enter array size");
            int size = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("enter array values separated by comma");
            string[] strarr = Console.ReadLine().Split(" ");

            int[] arr = new int[size];
            for (int i = 0; i < size; i++)
            {
                arr[i] = Convert.ToInt32(strarr[i]);
            }
            Console.WriteLine(s.GetMostOften(arr)); 

        }
    }
}
