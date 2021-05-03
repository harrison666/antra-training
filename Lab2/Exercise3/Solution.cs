using System;
using System.Collections.Generic;
using System.Text;

namespace Exercise3
{
    class Solution
    {
        public int GetPerfectSquare(int A, int B)
        {
            int i = Convert.ToInt32(Math.Ceiling(Math.Sqrt(A)));
            int count = 0;
            while (i * i <= B)
            {
                count++;
                i++;
            }
            return count;
        }
    }
}
