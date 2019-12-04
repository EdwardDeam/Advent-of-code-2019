using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;

namespace DayThree
{
  class MantattanDistance
  {
    static void Main(string[] args)
    {
      if (args == null || args.Length == 0)
      {
        Console.WriteLine("Program requires Input File as an Argument.");
        return;
      }
      List<List<Coord>> lines = LoadFile(args[0]);
      foreach (var line in lines)
      {
        System.Console.WriteLine("\n\n");
          foreach(var coord in line){
            System.Console.WriteLine(coord.ToString());
          }
      }
      

    }

    static List<List<Coord>> LoadFile(string path)
    {
      List<List<Coord>> results = new List<List<Coord>>();
      StreamReader sr = File.OpenText(path);
      {
        string s;
        while ((s = sr.ReadLine()) != null)
        {
          results.Add(GenerateCoordList(s));
        }
      }
      return results;
    }
    static List<Coord> GenerateCoordList(string input)
    {
      List<Coord> results = new List<Coord>();
      string[] data = input.Split(',');
      Regex rx = new Regex(@"(\w)(\d*)");
      MatchCollection matches = rx.Matches(input);
      // All lines start from the 'center'.
      Coord currentCoord = new Coord(0, 0);
      results.Add(currentCoord);
      foreach (Match m in matches)
      {
        GroupCollection groups = m.Groups;
        switch (groups[1].Value)
        {
            case "R":
            {
            Coord newCord = new Coord(currentCoord.x + System.Convert.ToInt32(groups[2].Value), currentCoord.y);
            results.Add(newCord);
            currentCoord = newCord;
            break;
            }
            case "L":
            {
            Coord newCord = new Coord(currentCoord.x - System.Convert.ToInt32(groups[2].Value), currentCoord.y);
            results.Add(newCord);
            currentCoord = newCord;
            break;
            }
            case "U":
            {
            Coord newCord = new Coord(currentCoord.x, currentCoord.y + System.Convert.ToInt32(groups[2].Value));
            results.Add(newCord);
            currentCoord = newCord;
            break;
            }
            case "D":
            {
            Coord newCord = new Coord(currentCoord.x, currentCoord.y -System.Convert.ToInt32(groups[2].Value));
            results.Add(newCord);
            currentCoord = newCord;
            break;
            }
        }
      }
      return results;
    }
  }
}
public struct Coord
{
  public int x, y;
  public Coord(int _x, int _y)
  {
    x = _x;
    y = _y;
  }
  public override string ToString() {
    return "X: " + x.ToString() + ", Y: " + y.ToString();
  }
}
