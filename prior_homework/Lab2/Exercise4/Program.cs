using System;

namespace Exercise4
{
    class Spiral
    {
        public void Display(int[,] matrix)
        {
            int up = 0, left = 0;
            int down = matrix.GetLength(0) - 1;
            int right = matrix.GetLength(1) - 1;

            // 0: go right   1: go down  2: go left  3: go up
            int direction = 0, i;

            while (left <= right && up <= down)
            {
                
                if (direction == 0)
                {
                    for (i = left; i <= right; i++)
                    {
                        Console.Write(matrix[up, i] + " "); 
                    }
                    up += 1;

                }
                if (direction == 1)
                {
                    for (i = up; i <= down; i++)
                    {
                        Console.Write(matrix[i, right] + " ");
                        
                    }
                    right -= 1;

                }
                if (direction == 2)
                {
                    for (i = right; i >= left; i--)
                    {
                        Console.Write(matrix[down, i] + " ");
                        
                    }
                    down -= 1;
                }
                if (direction == 3)
                {
                    for (i = down; i >= up; i--)
                    {
                        Console.Write(matrix[i, left] + " ");
                        
                    }
                    left += 1;
                }
                direction = (direction + 1) % 4;
            }
                
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Spiral s = new Spiral();
            int[,] matrix = new int[,] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };
            s.Display(matrix);
        }
    }
}
