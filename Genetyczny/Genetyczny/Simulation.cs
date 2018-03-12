using Genetyczny.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Genetyczny
{
    class Simulation
    {

        public static readonly int populationCount = 100;
        public static int[,] flowMatrix = null;
        public static int[,] distanceMatrix = null;
        public static int rowsCount = 0;
        
        
        
        static void Main(string[] args)
        {
            
            ReadInput();
            Population population = new Population(populationCount);
            population.PrintSolutions();
            
            Console.WriteLine("Best solution score : " + population.BestSolutionScore());
            Console.WriteLine("Worst solution score : " + population.WorstSolutionScore());
            Console.WriteLine("Average solution score : " + population.AverageSolution());
            
            Boolean breakpoint = true;
            
        }

        public static void ReadInput()
        {
            try
            {
                using (StreamReader sr = new StreamReader("matrix.txt"))
                {
                    String line = sr.ReadToEnd().Replace("  ", "").Replace("<", "").Replace(">", "").Replace("\r","");
                    var parts = line.Split('\n').ToList<string>();
                    InitializeMatrix(parts);
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

        public static void InitializeMatrix(List<string> list)
        {
            int length = int.Parse(list.ElementAt(0));
            rowsCount = length;
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

            //DO USUNIECIA
            Console.WriteLine("Macierz przeplywu");
            printMatrix(flowMatrix, length);
            Console.WriteLine("Macierz dystansu");
            printMatrix(distanceMatrix, length);
            //DO USUNIECIA
        }

        public static void printMatrix(int[,] matrix, int length)
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

       
    }
}
