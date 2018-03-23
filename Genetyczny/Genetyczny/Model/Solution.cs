using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

namespace Genetyczny.Model
{
    [Serializable]
    class Solution
    {
        #region Fields
        private static readonly Random random = new Random();
        private static readonly object syncLock = new object();
        int solutionId;

        public List<int> allocation = new List<int>();
        int estimatedScore = 0;

        #endregion
       
        #region Constructors
        public Solution(Solution solution, int id)
        {
            allocation = new List<int>();
            allocation = solution.allocation;
            solutionId = id;
            EstimateScore();
        }
        public Solution(int id)
        {
            solutionId = id;
        }
        public Solution()
        {
        }
        public Solution(Solution solution)
        {

            allocation = solution.allocation;
            solutionId = solution.solutionId;
            EstimateScore();
        }

        #endregion

        #region methods
        public Solution RandomAllocate(int matrixDimension)
        {
            int randomValue = 0;
            for(int i=0; i<matrixDimension; i++)
            {
                do
                {
                    randomValue = RandomNumber(0, matrixDimension);
                }while(allocation.Contains(randomValue));

                allocation.Add(randomValue);
            }
            EstimateScore();
            return this;
        }
       
        public void EstimateScore()
        {
            estimatedScore = 0;

            for (int row = 0; row < allocation.Count; row++)
            {
                for (int column = 0; column < allocation.Count; column++)
                {
                    estimatedScore += Simulation.GetValueFromDistanceMatrix(row, column)*Simulation.GetValueFromFlowMatrix(allocation.ElementAt(row), allocation.ElementAt(column));
                }
            }
        }

        public int GetEstimatedScore()
        {
            return estimatedScore;
        }

        public void Cross(Solution anotherSolution)
        {
            int pivot = allocation.Count / 2;
            for (int i = 0; i < pivot; i++)
            {
                allocation[pivot+i] = anotherSolution.allocation[pivot+i];
            }
            allocation = CorrectAllocations(allocation);
            EstimateScore();
        }

        public void Mutation()
        {
            var randomIndex = RandomNumber(0, allocation.Count);
            var temp = allocation[randomIndex];
            var randomIndexSecond = randomIndex;
            while (randomIndexSecond == randomIndex) {
                 randomIndexSecond = RandomNumber(0, allocation.Count);
            }
            allocation[randomIndex] = allocation[randomIndexSecond];
            allocation[randomIndexSecond] = temp;

            EstimateScore();
        }

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

        private List<int> CorrectAllocations(List<int> allocations)
        {
            List<int> correctAllocations = new List<int>(allocations.Count);
            List<int> list = new List<int>();
            int pivot = allocations.Count / 2;

            for (int i = 0; i < allocations.Count; i++) { list.Add(i); } //zainicjowanie listy intów od 0 do facilities.Length zeby pozniej z niej wykreslac
            for (int i = 0; i < allocations.Count; i++)
            {
                list.Remove(allocations[i]);
            }
            for (int i = 0; i < pivot; i++)
            { correctAllocations.Add(allocations[i]); }

            bool isDuplicate = false;
            for (int i = pivot; i < allocations.Count; i++)
            {
                isDuplicate = false;

                for (int j = i - 1; j >= 0; j--)
                {
                    if (allocations[i] == allocations[j])
                    {
                        isDuplicate = true;
                        correctAllocations.Add(list.ElementAt(0));
                        list.RemoveAt(0);
                    }
                }
                if (!isDuplicate)
                    correctAllocations.Add(allocations[i]);
            }
            return correctAllocations;
        }

        public bool IsListDistinct(List<int> list)
        {
            if (list.Count != list.Distinct().Count())
            {
                return false;
            }
            return true;
        }

        public void SetEstimatedScore(int score)
        {
            estimatedScore = score;
        }

        public void SetId(int id)
        {
            solutionId = id;
        }

        public int GetId()
        {
            return solutionId;
        }

        public Solution Copy() {

            Solution newSolution = new Solution();
            newSolution.allocation = this.allocation;
            newSolution.estimatedScore = this.estimatedScore;
            newSolution.SetId(this.GetId());
            return newSolution;

        }

        
        public static T DeepClone<T>(T obj)
        {
            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, obj);
                ms.Position = 0;

                return (T)formatter.Deserialize(ms);
            }
        }
        #endregion
    }
}
