using System;
using System.IO;

namespace DayTwo
{
  public class Solver
  {
    static void Main(string[] args)
    {
      if (args == null || args.Length == 0)
      {
        Console.WriteLine("Input File Required.");
        return;
      }
      string data = LoadFile(args[0]);
      IntComputer vm = new IntComputer(data);
      // Part One.
      int answer = vm.Run(12, 2);
      Console.WriteLine("Part One: " + answer.ToString());
      //Part Two.
      NounVerb nv = Crack(vm, 99, 19690720);
      answer = 100 * nv.Noun + nv.Verb;
      Console.WriteLine("Part Two: " + answer.ToString());
    }

    static NounVerb Crack(IntComputer vm, int maxValue, int goal)
    {
      // Reversing as challenge goal what a larger number.
      for (int n = maxValue; n >= 0; n--)
      {
        for (int v = maxValue; v >= 0; v--)
        {
          if (vm.Run(n, v) == goal)
          {
            return new NounVerb(n, v);
          }
        }
      }
      return null;
    }
    
    public static string LoadFile(string path)
    {
      try
      {
        StreamReader sr = new StreamReader(path);
        return sr.ReadToEnd();
      }
      catch (IOException e)
      {
        Console.WriteLine("The file could not be read: ");
        Console.WriteLine(e.Message);
        return null;
      }
    }
  }
  public class NounVerb
  {
    public int Noun { get; }
    public int Verb { get; }
    public NounVerb(int a, int b)
    {
      Noun = a;
      Verb = b;
    }
  }
}
