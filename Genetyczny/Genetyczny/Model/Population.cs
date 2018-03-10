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

        //public int AverageSolution() { }
        //public int BestSolution() { }
        //public int WorstSolution() { }

        public void PrintSolutions()
        {
            foreach (Solution solution in solutions)
            {
                solution.Print();
                Console.WriteLine("");
            }
            
        }
        
    }
}
