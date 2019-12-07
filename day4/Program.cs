using System.Collections.Generic;

namespace DayFour
{
  class Program
  {
    static void Main(string[] args)
    {
      int start = 178416;
      int end = 676461;
      List<int> passwords = new List<int>();
      for (int i = start; i <= end; i++)
      {
        bool decreases = DecreaseTest(i);
        bool matches = MatchTest(i);
        if(decreases && matches)
        {
          passwords.Add(i);
        }
      }
      System.Console.WriteLine("Matches: " + passwords.Count.ToString());
    }
    private static bool DecreaseTest(int number)
    {
      int previous = number % 10;
      number /= 10;
      while(number > 0)
      {
        int test = number % 10;
        // System.Console.WriteLine(previous);
        if(previous < test)
        {
          return false;
        }
        previous = test;
        number /= 10;
      }
      return true;
    }
    private static bool MatchTest(int number)
    {
      bool matched = false;
      int previous = number % 10;
      number /= 10;
      while(number > 0)
      {
        int current = number % 10;
        // System.Console.WriteLine(previous);
        // System.Console.WriteLine(current + "\n");
        if(previous == current)
        {
          matched = true;
        }
        previous = current;
        number /= 10;
      }
      return matched;
    }
  }
}
