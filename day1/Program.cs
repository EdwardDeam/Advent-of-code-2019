using System;
using System.IO;

namespace DayOne
{
    class RocketModule
    {
        public static void Main(string[] args)
        {
            try
            {
                if(args == null || args.Length == 0)
                {
                    Console.WriteLine("Program requires Input File as an Argument.");
                    throw new ArgumentException("No Input File Specified.");
                }

                int totalFuelCount = 0;
                StreamReader sr = File.OpenText(args[0]);
                {   
                    string s;
                    while ((s = sr.ReadLine()) != null)
                    {
                        totalFuelCount += TotalFuelRequirement(System.Convert.ToInt32(s));
                    }
                }

                Console.WriteLine("Total Fuel Required: " + totalFuelCount.ToString());
            }
            catch (Exception e )
            {
                Console.WriteLine("Error in Main: " + e.Message);
            }
        }

        public static int FuelRequirement(int mass)
        {
            // Calcutates fuel required based on input mass.
            return (int)(MathF.Floor(mass / 3.0f) - 2.0f);
        }

        public static int TotalFuelRequirement(int mass)
        {
            int total = 0;
            // Loop until no more fuel is required
            while(mass > 0)
            {
                mass = FuelRequirement(mass);
                // Ignore any negative fuel costs.
                total += mass > 0 ? mass : 0;
            }
            return total;
        }
    }
}
