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

        List<int> allocation = new List<int>();
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
            int randomValue = 0;
            for(int i=0; i<nodesCount; i++)
            {
                do
                {
                    randomValue = RandomNumber(0, nodesCount);
                }while(allocation.Contains(randomValue));

                allocation.Add(randomValue);
            }
        }
       
        public int EstimateScore()
        {
            for (int row = 0; row < allocation.Count; row++)
            {
                for (int column = 0; column < allocation.Count; column++)
                {
                    estimatedScore += Simulation.GetValueFromDistanceMatrix(row, column)*Simulation.GetValueFromFlowMatrix(allocation.ElementAt(row), allocation.ElementAt(column));
                }
            }
            return estimatedScore;
        }

        public int GetEstimatedScore()
        {
            return estimatedScore;
        }

        public void Cross(Solution anotherSolution)
        {
            int pivot = this.allocation.Count / 2;
            for (int i = 0; i < pivot; i++)
            {
                int temp = allocation[i];
                allocation[i] = anotherSolution.allocation[allocation.Count - 1 - i];
                anotherSolution.allocation[allocation.Count - 1 - i] = temp;
            }
        }

        public void Mutation()
        {
            var randomIndex = RandomNumber(0, allocation.Count-1);
            var temp = allocation[randomIndex];
            var randomIndexSecond = 0;
            while (randomIndexSecond != randomIndex) {
                RandomNumber(0, allocation.Count - 1);
            }
            allocation[randomIndex] = allocation[randomIndexSecond];
            allocation[randomIndexSecond] = temp;
        }

        public void Select() { }

        public void Print()
        {
            Console.WriteLine("Score: " + estimatedScore);
            foreach (int factoryNumber in allocation)
            {
                Console.WriteLine(factoryNumber);
            }
        }

        public static int RandomNumber(int min, int max)
        {
            lock (syncLock)
            {
                return random.Next(min, max);
            }
        }

        private int[] CorrectFacilities(int[] facilities)
        {
            int[] newFacilities = new int[facilities.Length];
            List<int> list = new List<int>();

            for (int i = 0; i < facilities.Length; i++) { list.Add(i); } //zainicjowanie listy intów od 0 do facilities.Length zeby pozniej z niej wykreslac
            for (int i = 0; i < facilities.Length; i++)
            {
                list.Remove(facilities[i]);
            }

            for (int i = 0; i < facilities.Length; i++)
            {
                for (int j = i; j > 0; j--)
                {
                    if (facilities[i] == facilities[j])
                    {
                        facilities[i] = list.ElementAt(0);
                        list.RemoveAt(0);
                    }
                }
            }

            return newFacilities;
        }
    }
}
