using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genetyczny.Model
{
    class Solution
    {
        List<KeyValuePair<int, int>> allocation = new List<KeyValuePair<int, int>>();


        public Solution(int nodesCount)
        {
            RandomAllocate(Simulation.rowsCount);
        }

        //public int Estimate(Dictionary<int, char> allocation) {}
        //public Dictionary<int, char> Cross(Solution anotherSolution){}
        public void RandomAllocate(int nodesCount)
        {
            int column;
            int row;
            for (int i = 0; i < nodesCount; i++)
            {
                Random rnd = new Random();
                do
                {
                    row = rnd.Next(nodesCount+1);
                    column = rnd.Next(row+1);
                    
                }
                while (column!=row && !allocation.Contains(new KeyValuePair<int, int>(column, row)));
                
                allocation.Add(new KeyValuePair<int, int>(column,row));
            }
        }

        public static bool IsPossibleFlow(int key, int value)
        {
            return Simulation.flowMatrix[key, value] != 0;
        }
        public static bool IsPossibleDistance(int key, int value)
        {
            return Simulation.distanceMatrix[key, value] != 0;
        }
        public void Print()
        {
            foreach (KeyValuePair<int, int> pair in allocation)
            {
                Console.WriteLine(pair.Key.ToString() + "-" + pair.Value.ToString());
            }
        }
    }
}
