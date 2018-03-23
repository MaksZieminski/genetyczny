using Genetyczny.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Genetyczny
{
    class Simulation
    {
        #region fields
        public static readonly int populationCount = 100;
        public static readonly string matrixFileName = "matrix2.txt";
        public static int[,] flowMatrix;
        public static int[,] distanceMatrix;
        public static int matrixDimension = 0;

        #endregion

        #region methods
        static void Main(string[] args)
        {
            var csv = new StringBuilder();
            ParseMatrixFromFile();
            Population population = new Population(populationCount);
            population.PrintPopulationInfo();
            

            using (var w = new StreamWriter("ruletkaNowa.csv"))
            {
                while (population.generation < 100)
                {
                    Population newPopulation = new Population();
                    for (int i = 1; i < population.solutions.Count+1; i++)
                    {
                        Solution newSolution = new Solution();
                        //newSolution = Solution.DeepClone<Solution>(population.SelectSolutionByTournament());
                        newSolution = Solution.DeepClone<Solution>(population.solutions[population.SelectSolution()]);

                        int randomNumber = Solution.RandomNumber(0, 100);
                        if (!(newSolution.GetEstimatedScore() == population.BestSolutionScore()))
                        {
                            if (randomNumber < population.crossChancePercentage)
                            {
                                Solution partner = new Solution();
                                 partner=     Solution.DeepClone<Solution>(population.solutions[population.SelectSolution()]);
                                if (partner.GetEstimatedScore() != newSolution.GetEstimatedScore())
                                    newSolution.Cross(partner);

                            }
                            
                            
                        
                        }
                        if (randomNumber < population.mutationChancePercentage)
                        {
                            newSolution.Mutation();
                        }
                        newSolution.SetId(i);
                        newPopulation.solutions.Add(newSolution);
                    }
                    
                    newPopulation.generation = population.generation + 1;
                    population = newPopulation;
                    population.EstimatePopulationScore();
                    population.PrintPopulationInfo();
                    //population.WriteToCsv();

                    ////DO USUNIECIA
                    // PrintFlowAndDistanceMatrix();
                    var line = string.Format("{0}ss{1}",  population.BestSolutionScore(), population.generation);
                    w.WriteLine(line);
                    w.Flush();
                    
                }
                }
        }

        public static void ParseMatrixFromFile()
        {
            try
            {
                using (StreamReader sr = new StreamReader(matrixFileName))
                {
                    String line = sr.ReadToEnd().Replace("  ", "").Replace("<", "").Replace(">", "").Replace("\r","");
                    var parts = line.Split('\n').ToList<string>();
                    FillMatrixes(parts);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read: " + e.Message);
            }

            
        }

        public static void PrintStringArray(List<string> list)
        {
            int counter = 0;
            foreach (string element in list)
            {
                Console.WriteLine("Nr elementu w liscie:" + counter + ". : " + element);
                counter++;
            }


        }

        public static void FillMatrixes(List<string> list)
        {
            int length = int.Parse(list.ElementAt(0));
            matrixDimension = length;
            list.RemoveAt(0);
           
            distanceMatrix = new int[length, length];
            flowMatrix = new int[length, length];

            for (int column = 0; column < length; column++)
            {
                for (int row = 0; row < length; row++)
                {
                    flowMatrix[column, row] = int.Parse(list.ElementAt(column).ElementAt(row).ToString());
                    distanceMatrix[column, row] = int.Parse(list.ElementAt(column+length).ElementAt(row).ToString());
                }
            }
        }

        public static void PrintMatrix(int[,] matrix, int length)
        {
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    Console.Write(matrix[i, j]+" ");
                }
                Console.WriteLine();
            }
        }

        public static int GetValueFromFlowMatrix(int row, int column)
        {
            return flowMatrix[row, column];
        }

        public static int GetValueFromDistanceMatrix(int row, int column)
        {
            return distanceMatrix[row, column];
        }

        public static void PrintFlowAndDistanceMatrix()
        {
            Console.WriteLine("Macierz przeplywu");
            PrintMatrix(flowMatrix, matrixDimension);
            Console.WriteLine("Macierz dystansu");
            PrintMatrix(distanceMatrix, matrixDimension);
        }
        #endregion methods

    }
}
