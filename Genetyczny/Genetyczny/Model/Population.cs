using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genetyczny.Model
{
    class Population
    {

        #region fields
        public List<Solution> solutions = new List<Solution>();
        int populationCount = 0;
        #endregion

        #region Constructors
        public Population(int populationCount)
        {
            Initialize(populationCount);
        }
        #endregion


        public int Count()
        {
            return populationCount;
        }

        public void Initialize(int populationCount)
        {
            this.populationCount = populationCount;

            for (int i = 0; i < populationCount; i++)
            {
                solutions.Add(new Solution(populationCount));
            }
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
        
    }
}
