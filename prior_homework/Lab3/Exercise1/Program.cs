using System;

namespace Exercise1
{
    abstract class Shape1
    {
        protected float R, L, B;

        //Abstract methods can have only declarations
        public abstract float Area();
        public abstract float Circumference();

    }
    class Rectangle : Shape1
    {
        public void GetInput()
        {
            Console.Write("Enter length: ");
            L = (float)Convert.ToDouble(Console.ReadLine());
            Console.Write("Enter breadth: ");
            B = (float)Convert.ToDouble(Console.ReadLine());
        }
        public override float Area()
        {
            return L * B;
        }

        public override float Circumference()
        {
            return (L + B) * 2;
        }
    }

    class Circle : Shape1
    {
        public void GetInput()
        {
            Console.Write("Enter radius: ");
            R = (float)Convert.ToDouble(Console.ReadLine());
        }
        public override float Area()
        {
            return (float)(R * R * Math.PI);
        }

        public override float Circumference()
        {
            return (float)(2 * R * Math.PI);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Rectangle r = new Rectangle();
            r.GetInput();
            Calculate(r);

            Circle c = new Circle();
            c.GetInput();
            Calculate(c);
        }

        public static void Calculate(Shape1 S)
        {

            Console.WriteLine("Area : {0}", S.Area());
            Console.WriteLine("Circumference : {0}", S.Circumference());

        }

    }
}
