using Genetyczny.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genetyczny
{
    class Simulation
    {

        public static readonly int populationCount = 100;


        static void Main(string[] args)
        {
            Population population = new Population(populationCount);

        }
    }
}
