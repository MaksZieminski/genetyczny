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



        static void Main(string[] args)
        {
            Population population = new Population(populationCount);
            ReadInput();
            Boolean breakpoint = true;

        }

        public static void ReadInput()
        {

            try
            {   
                using (StreamReader sr = new StreamReader("matrix.txt"))
                {
                    String line = sr.ReadToEnd();
                    List<string> parts = line.Replace(">"," ").Split('<').ToList<string>();
                    PrintStringArray(parts);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read: " + e.Message);
            }

            
        }

        public static void PrintStringArray(List<string> list)
        {
            foreach (string element in list)
            {
                Console.WriteLine(element);
            }
        }

    }
}
