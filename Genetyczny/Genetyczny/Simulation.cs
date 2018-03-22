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
        public static readonly string matrixFileName = "matrix1.txt";
        public static int[,] flowMatrix;
        public static int[,] distanceMatrix;
        public static int matrixDimension = 0;
        
        #endregion


        static void Main(string[] args)
        {
            var csv = new StringBuilder();
            ParseMatrixFromFile();
            Population population = new Population(populationCount);
            population.Initialize();
            population.PrintPopulationInfo();

            using (var w = new StreamWriter("zupelnieNoweRozwiazani3334444444444444444444.csv"))
            {
                while (population.BestSolutionScore() > 1653)
                {
                    Population newPopulation = new Population();
                    for (int i = 0; i < populationCount; i++)
                    {
                        int selectedId = population.SelectSolution();
                        Solution newSolution = new Solution(i + 1);
                        newSolution = Solution.DeepClone<Solution>(population.solutions.ElementAt(selectedId));

                        int randomNumber = Solution.RandomNumber(0, 100);
                        if (!(newSolution.GetEstimatedScore() == population.BestSolutionScore()))
                            {
                            if (randomNumber < population.crossChancePercentage)
                            {
                                Solution crossSolution = Solution.DeepClone<Solution>(population.solutions[population.SelectSolution()]);
                                if (crossSolution.GetEstimatedScore() != newSolution.GetEstimatedScore())
                                    newSolution.Cross(crossSolution);

                            }
                           
                        }
                        if (randomNumber < population.mutationChancePercentage)
                        {
                            newSolution.Mutation();
                        }
                        newSolution.SetId(i + 1);
                        newPopulation.solutions.Add(newSolution);

                    }


                    newPopulation.generation = population.generation + 1;
                    population = newPopulation;
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


    }
}
