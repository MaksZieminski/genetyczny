using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genetyczny.Model
{
    class Solution
    {
        int[,] flowMatrix = Simulation.flowMatrix;
        int[,] distanceMatrix = Simulation.distanceMatrix;
        Dictionary<int, int> allocation = new Dictionary<int, int>();


        public Solution(int nodesCount)
        {
            RandomAllocate(allocation);
        }

        //public int Estimate(Dictionary<int, char> allocation) {}
        //public Dictionary<int, char> Cross(Solution anotherSolution){}
        public static void RandomAllocate(Dictionary<int, int> allocation)
        {
            int column;
            int row;
            for (int i = 0; i < allocation.Count; i++)
            {
                Random rnd = new Random();
                do
                {
                    column = rnd.Next(allocation.Count);
                    row = rnd.Next(allocation.Count);
                }
                while (column==row && allocation.ContainsKey(column) && allocation.ContainsValue(row));

                allocation.Add(column, row);
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
    }
}
