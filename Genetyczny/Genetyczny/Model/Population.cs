using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Genetyczny.Model
{
    class Population
    {

        #region fields
        public double crossChancePercentage = 55;
        public double mutationChancePercentage = 3;
        public List<Solution> solutions = new List<Solution>();
        int populationCount = 0;
        public int generation = 0;

        #endregion

        #region Constructors
        public Population(int populationCount)
        {
            this.populationCount = populationCount;
        }

        public Population()
        {
        }
        #endregion

        #region Methods
        public int Count()
        {
            return populationCount;
        }

        public void Initialize()
        {
            generation = 1;
            for (int i = 0; i < populationCount; i++)
            {
                solutions.Add(new Solution(i + 1).RandomAllocate(Simulation.matrixDimension));
            }
            EstimatePopulationScore();
        }

        public double AverageSolution()
        {
            double averageScore = 0;
            int count = solutions.Count;
            foreach (Solution solution in solutions)
            {
                averageScore += solution.GetEstimatedScore();
            }
            return averageScore / count;
        }

        public int WorstSolutionScore()
        {
            int score = 0;
            foreach (Solution solution in solutions)
            {
                if (score < solution.GetEstimatedScore())
                {
                    score = solution.GetEstimatedScore();
                }
            }
            return score;
        }

        public int BestSolutionScore()
        {
            int score = int.MaxValue;
            foreach (Solution solution in solutions)
            {
                if (score > solution.GetEstimatedScore())
                {
                    score = solution.GetEstimatedScore();
                }
            }
            return score;
        }

        public void PrintSolutions()
        {
            int counter = 1;
            foreach (Solution solution in solutions)
            {
                // Console.WriteLine("Solution[" + counter + "]. Score : " + solution.GetEstimatedScore());
                solution.Print();
                Console.WriteLine("");
                counter++;
            }

        }

        public void PrintPopulationInfo()
        {
            Console.WriteLine("Generation:" + generation);
            Console.Write("Best:" + BestSolutionScore());
            Console.Write(";Worst:" + WorstSolutionScore());
            Console.WriteLine(";Average:" + AverageSolution());
        }

        public void EstimatePopulationScore()
        {
            for (int i = 0; i < solutions.Count; i++)
            {
                solutions[i].EstimateScore();
            }
        }

        public void WriteToCsv() {
            using (var w = new StreamWriter("results3.csv"))
            {
                    var line = string.Format("{0},{1}", generation, BestSolutionScore());
                    w.WriteLine(line);
                    w.Flush();
                
            }

        }

        public void ResetPopulationScore()
        {
            solutions.ForEach(s => s.SetEstimatedScore(0));
        }

        public int SelectSolution()
        {
            List<int> idSolutions = new List<int>();
            foreach (Solution s in solutions)
            {
                int averageScore = BestSolutionScore();
                for (int i = averageScore * 2; i > s.GetEstimatedScore(); i--)
                {
                    idSolutions.Add(s.GetId());
                }
            }
            int random = Solution.RandomNumber(0, idSolutions.Count);
            int id = idSolutions.ElementAt(random);

            return id - 1;

        }

        public int SelectSolutionByTournament()
        {
            List<Solution> listOfSolution = new List<Solution>();
            List<int> listOfNumbers = new List<int>();

            for (int i = 0; i < 10; i++)
            {
                int randomId = Solution.RandomNumber(0, 100);
                while (listOfNumbers.Contains(randomId))
                {
                    listOfNumbers.Add(randomId);
                }
            }
        }
        public bool AreSolutionsDistinct()
        {
            bool oby = true;
            for (int i = 0; i < solutions.Count; i++)
            {
                if (!solutions[i].IsListDistinct(solutions[i].allocation))
                {
                    oby = false;
                }
            }
            return oby;
        }
        #endregion
    }
}
