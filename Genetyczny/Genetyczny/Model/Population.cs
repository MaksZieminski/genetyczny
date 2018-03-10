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
        private List<Solution> solutions = new List<Solution>();
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
            for (int i = 0; i < populationCount; i++)
            {
                solutions.Add(new Solution(populationCount));
            }
        }


        //public int AverageSolution() { }
        //public int BestSolution() { }
        //public int WorstSolution() { }
        
    }
}
