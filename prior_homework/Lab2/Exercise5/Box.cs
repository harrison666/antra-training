using System;
using System.Collections.Generic;
using System.Text;

namespace Exercise5
{
    class Box
    {
        
        double length, breadth, height;

        public Box()
        {

        }
        public Box(double len, double bre, double hei)
        {
            length = len;
            breadth = bre;
            height = hei;
        }

        public static Box operator +(Box b, Box c)
        {
            Box box = new Box();
            box.length = b.length + c.length;
            box.breadth = b.breadth + c.breadth;
            box.height = b.height + c.height;
            return box;
        }

        public double getVolume()
        {
            return length * breadth * height;
        }
        public void setLength(double len)
        {
            length = len;
        }

        public void setBreadth(double bre)
        {
            breadth = bre;
        }

        public void setHeight(double hei)
        {
            height = hei;
        }


    }
}
