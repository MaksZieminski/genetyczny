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
        public static int[,] flowMatrix;
        public static int[,] distanceMatrix;
        public static int matrixDimension = 0;
        
        static void Main(string[] args)
        {
            
            ParseMatrixFromFile();
            Population population = new Population(populationCount);
            population.Start();

            
            ////DO USUNIECIA
            //Console.WriteLine("Macierz przeplywu");
            //printMatrix(flowMatrix, length);
            //Console.WriteLine("Macierz dystansu");
            //printMatrix(distanceMatrix, length);
            ////DO USUNIECIA


            Boolean breakpoint = true;
            
        }

        public static void ParseMatrixFromFile()
        {
            try
            {
                using (StreamReader sr = new StreamReader("matrix.txt"))
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
