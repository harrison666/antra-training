using System;

namespace Exercise5
{
    class Program
    {
        static void Main(string[] args)
        {
            Box box1 = new Box(3, 7, 10);
            Box box2 = new Box(6, 13, 20);

            double volume;

            //volume of box 1
            volume = box1.getVolume();
            Console.WriteLine($"Volume of box1 : {volume}");

            //volume of box 2
            volume = box2.getVolume();
            Console.WriteLine($"Volume of box2 : {volume}");

            //Add two object as follows:
            Box box3 = box1 + box2;

            //volume of box 3
            volume = box3.getVolume();
            Console.WriteLine($"Volume of box3 : {volume}");
            Console.ReadKey();
        }
    }
}
