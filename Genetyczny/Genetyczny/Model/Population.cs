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
        int generation = 0;
        #endregion

        #region Constructors
        public Population(int populationCount)
        {
            this.populationCount = populationCount;
        }
        #endregion


        public int Count()
        {
            return populationCount;
        }

        public void Initialize()
        {

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

        public void Select(List<Solution> population) { }

        public void Start()
        {
            Initialize();
            while (true)
            {
                Select(solutions);
                //Cross(solutions);
                //Mute(solutions);
                //EstimateScore for population
                PrintPopulationInfo();
            }

        }

        public void PrintPopulationInfo()
        {
            Console.WriteLine("Best solution score : " + BestSolutionScore());
            Console.WriteLine("Worst solution score : " + WorstSolutionScore());
            Console.WriteLine("Average solution score : " + AverageSolution());
        }

        public void EstimatePopulationScore()
        {
            for(int i=0; i < solutions.Count; i++)
            {
                solutions[i].EstimateScore();
            }
        }
    }
}
