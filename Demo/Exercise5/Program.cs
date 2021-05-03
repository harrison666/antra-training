using System;

namespace Exercise5
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter the Number of Rows:");
            int nrow = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("1");
            for (int i = 1; i < nrow; i++)
            {
                if ((i - 1) % 4 < 2)
                {
                    for (int j = 0; j <= i; j++)
                    {
                        if (j % 2 == 0)
                        {
                            Console.Write("0");
                        } else
                        {
                            Console.Write("1");
                        }
                    }
                        
                } else
                {
                    for (int j = 0; j <= i; j++)
                    {
                        if (j % 2 == 0)
                        {
                            Console.Write("1");
                        }
                        else
                        {
                            Console.Write("0");
                        }
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
