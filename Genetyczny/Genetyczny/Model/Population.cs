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
            EstimatePopulationScore();
            PrintPopulationInfo();
            //while (true)
            //{

            while (true) { 
            CrossSolutions();

                //Mute(solutions);
                EstimatePopulationScore();
                PrintPopulationInfo();
                WriteToCsv();
                
            }

        }

        public void PrintPopulationInfo()
        {
            Console.Write("Best : " + BestSolutionScore());
            Console.Write("Worst : " + WorstSolutionScore());
            Console.WriteLine("Average : " + AverageSolution());
        }

        public void EstimatePopulationScore()
        {
            for (int i=0; i < solutions.Count; i++)
            {
                solutions[i].EstimateScore();
            }
        }

        public void WriteToCsv() {

            //before your loop
            //var csv = new StringBuilder();

            ////in your loop
            //var first = reader[0].ToString();
            //var second = image.ToString();
            ////Suggestion made by KyleMit
            //var newLine = string.Format("{0},{1}", first, second);
            //csv.AppendLine(newLine);

            ////after your loop
            //File.WriteAllText(filePath, csv.ToString());

        }

        public void ResetPopulationScore()
        {
            solutions.ForEach(s => s.SetEstimatedScore(0));
        }

        public void CrossSolutions()
        {
            for (int i = 0; i + 1 < solutions.Count; i++)
            {
                solutions[i].Cross(solutions[i + 1]);
            }
        }
        
    }
}
