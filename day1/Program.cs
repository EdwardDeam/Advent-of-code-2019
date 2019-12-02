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

                int fuelCount = 0;
                StreamReader sr = File.OpenText(args[0]);
                {   
                    string s;
                    while ((s = sr.ReadLine()) != null)
                    {
                        fuelCount += FuelRequirement(System.Convert.ToInt32(s));
                    }
                }

                Console.WriteLine("Total Fuel Required: " + fuelCount.ToString());
            }
            catch (Exception e )
            {
                Console.WriteLine("Error in Main: " + e.Message);
            }
        }
        
        public static int FuelRequirement(int mass)
        {
            return (int)(MathF.Floor(mass / 3.0f) - 2.0f);
        }
    }
}
