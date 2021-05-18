using System;
using System.Collections.Generic;
using System.Text;

namespace Exercise2
{
    class Solution
    {
        public int GetMostOften(int[] A)
        {
            var counts = new Dictionary<int, int>();
            foreach (int number in A)
            {
                int count;
                counts.TryGetValue(number, out count);
                count++;
                //Automatically replaces the entry if it exists;
                //no need to use 'Contains'
                counts[number] = count;
            }
            int mostCommonNumber = 0, occurrences = 0;
            foreach (var pair in counts)
            {
                if (pair.Value > occurrences)
                {
                    occurrences = pair.Value;
                    mostCommonNumber = pair.Key;
                }
            }
            return mostCommonNumber;
        }
    }
}
