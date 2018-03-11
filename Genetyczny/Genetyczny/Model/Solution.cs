using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genetyczny.Model
{
    class Solution
    {
        #region Fields
        private static readonly Random random = new Random();
        private static readonly object syncLock = new object();

        List<KeyValuePair<int, int>> allocation = new List<KeyValuePair<int, int>>();
        int estimatedScore = 0;

        #endregion
       
        #region Constructors
        public Solution(int nodesCount)
        {
            RandomAllocate(Simulation.rowsCount);
            estimatedScore = EstimateScore();

        }

        public Solution() { }
        #endregion

        
        public void RandomAllocate(int nodesCount)
        {
            int column;
            int row;
            for (int i = 0; i < nodesCount; i++)
            {
                do
                {
                    row = RandomNumber(0, nodesCount);
                    column = RandomNumber(0, row);

                }
                while (column == row || allocation.Contains(new KeyValuePair<int, int>(row, column)) || !IsPossibleFlow(row, column) || !IsPossibleDistance(row, column));

                allocation.Add(new KeyValuePair<int, int>(row, column));
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

        public static int RandomNumber(int min, int max)
        {
            lock (syncLock)
            { // synchronize
                return random.Next(min, max);
            }
        }

        public int EstimateScore()
        {
            int points = 0;

            foreach (KeyValuePair<int, int> pair in allocation)
            {
                points += Simulation.GetValueFromFlowMatrix(pair.Key, pair.Value) * Simulation.GetValueFromDistanceMatrix(pair.Key, pair.Value);
            }
            return points;
        }

        public int GetEstimatedScore()
        {
            return estimatedScore;
        }

    }
}
