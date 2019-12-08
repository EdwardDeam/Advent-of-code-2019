using System.Collections.Generic;
using System.Linq;
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
        if (decreases && matches)
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
      while (number > 0)
      {
        int test = number % 10;
        if (previous < test)
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
      List<int> nums = ToDigitList(number);
      var groups = nums.GroupBy(v => v);
      foreach (var i in groups)
      {
        if (i.Count() == 2)
        {
          matched = true;
          break;
        }
      }
      return matched;
    }
    private static List<int> ToDigitList(int i)
    {
      List<int> result = new List<int>();
      while (i != 0)
      {
        result.Add(i % 10);
        i /= 10;
      }
      result.Sort();
      return result;
    }
  }
}
