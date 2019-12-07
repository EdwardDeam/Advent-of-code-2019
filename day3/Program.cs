using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;

namespace DayThree
{
  class ManhattanDistance
  {
    static void Main(string[] args)
    {
      if (args == null || args.Length == 0)
      {
        Console.WriteLine("Program requires Input File as an Argument.");
        return;
      }
      List<List<Coord>> paths = GeneratePathFromFile(args[0]);
      // System.Console.WriteLine("PATHS");
      // foreach (var line in paths)
      // {
      //   System.Console.WriteLine("\n");
      //     foreach(var coord in line){
      //       System.Console.WriteLine(coord.ToString());  
      //     }
      // }
      List<Coord> intersections = FindIntersections(paths);
      // System.Console.WriteLine("\nINTERSECTIONS");
      // foreach(var coord in intersections){
      //   System.Console.WriteLine(coord.ToString() );
      // }
      int manhattanDist;
      Coord closest = ClosestCoord(intersections, out manhattanDist);
      int numSteps = FindLeastSteps(intersections, paths);
      System.Console.WriteLine("\nThe Shorteset Distance is: " + manhattanDist.ToString());
      System.Console.WriteLine("The number of steps is: " + numSteps.ToString());
    }

    private static int FindLeastSteps(List<Coord> intersections, List<List<Coord>> paths)
    {
      int shortest = int.MaxValue;
      foreach(Coord goal in intersections)
      {
        if(goal.x == 0 && goal.y ==0)
        {
          continue;
        }
        System.Console.WriteLine("NEW");
        int dist = FindSteps(goal, paths);
        System.Console.WriteLine("|||||LEAST DIST: " + dist.ToString());
        if(dist < shortest)
        {
          shortest = dist;
        }
      }
      return shortest;
    }
    private static int FindSteps(Coord goal, List<List<Coord>> paths)
    {
      System.Console.WriteLine("PATH A");
      int pathASteps = TraversePath(goal, paths[0]);
      System.Console.WriteLine("PATH B");
      int pathBSteps = TraversePath(goal, paths[1]);
      return pathASteps + pathBSteps;
    }
    private static int TraversePath(Coord goal, List<Coord> path)
    {
      System.Console.WriteLine("GOAL******: " + goal.ToString());
      int steps = 0;
      Coord previous = path[0];
      for (int i = 1; i < path.Count; i++)
      {
        Coord current = path[i];
        // Find orientation of line.
        int dx = previous.x - current.x;
        int dy = previous.y - current.y;
        System.Console.WriteLine("DX: " + dx.ToString() + ", DY: " + dy.ToString());
        if (dx == 0) // Vertical
        {
          // direction to goal
          System.Console.WriteLine("Vertical");
          if (goal.x == current.x) // lines are on the same x plane
          {
            if (InRange(goal.y, previous.y, current.y))
            {
              // The Goal has been hit.
              // int absY = dy > 0 ? dy : -1 * dy;
              int absPrevY = previous.y > 0 ? previous.y : -1 * previous.y;
              int absGoalY = goal.y > 0 ? goal.y : -1 * goal.y;
              if(absPrevY < goal.y)
              {
                // Heading Up
                int testdist = goal.y - previous.y;
                steps += testdist > 0 ? testdist : -1 * testdist;
                System.Console.WriteLine("1.1This Step: " + testdist);
                break;
              }
              else
              {
                System.Console.WriteLine("CURRENT: " + previous.ToString());
                System.Console.WriteLine("G: " + goal.y + ", C: " + previous.y);
                int testdist = goal.y - previous.y;
                steps += testdist > 0 ? testdist : -1 * testdist;
                System.Console.WriteLine("1.2This Step: " + testdist);
                break;
              }
            }
          }
          else
          {
            // Add the distance to steps
            int test = dy > 0 ? dy : -1 * dy;
            System.Console.WriteLine("2This Step: " + test);
            steps += test;
          }
        }
        else // Horizontal
        {
          // System.Console.WriteLine("Horizontal");
          if (goal.y == current.y) // lines are on the same y plane
          {
            if (InRange(goal.x, previous.x, current.x))
            {
              // The Goal has been hit.
              // int absY = dy > 0 ? dy : -1 * dy;
              int absPrevX = previous.x > 0 ? previous.x : -1 * previous.x;
              int absGoalX = goal.x > 0 ? goal.x : -1 * goal.x;
              if(absPrevX < goal.x)
              {
                // Heading Right
                int testdist = previous.x - goal.x;
                steps += testdist > 0 ? testdist : -1 * testdist;
                System.Console.WriteLine("3.1This Step: " + testdist);
                break;
              }
              else
              {
                // Heading Left
                int testdist = goal.x - previous.x;
                steps += testdist > 0 ? testdist : -1 * testdist;
                System.Console.WriteLine("3.2This Step: " + testdist);
                break;
              }
            }
          }
          else
          {
            // Add the distance to steps
            int test = dx > 0 ? dx : -1 * dx;
            System.Console.WriteLine("4This Step: " + test);
            steps += test;
          }
        }
        System.Console.WriteLine("Steps: " + steps.ToString() + "\n");
        previous = current;
      }
      return steps;
    }
    static List<List<Coord>> GeneratePathFromFile(string path)
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
    private static List<Coord> GenerateCoordList(string input)
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
              Coord newCord = new Coord(currentCoord.x, currentCoord.y - System.Convert.ToInt32(groups[2].Value));
              results.Add(newCord);
              currentCoord = newCord;
              break;
            }
        }
      }
      return results;
    }

    private static List<Coord> FindIntersections(List<List<Coord>> paths)
    {
      List<Coord> intersections = new List<Coord>();
      List<Coord> pathA = paths[0];
      List<Coord> pathB = paths[1];
      Coord previous1 = pathA[0];
      for (int i = 1; i < pathA.Count; i++)
      {
        Coord current1 = pathA[i];
        Coord previous2 = pathB[0];
        for (int j = 1; j < pathB.Count; j++)
        {
          Coord current2 = pathB[j];
          if (CompareLines(previous1, current1, previous2, current2))
          {
            var t = GetIntersectingCoord(previous1, current1, previous2, current2);
            intersections.Add(t);
          }
          previous2 = current2;
        }
        previous1 = current1;
      }

      return intersections;
    }

    private static bool CompareLines(Coord a1, Coord a2, Coord b1, Coord b2)
    {
      // Decide the Direction of line A. If the delta X is 0 then the line is vertical.
      int dax = a1.x - a2.x;
      int dbx = b1.x - b2.x;
      // Assume that the lines dont run parallel for now.
      if (dax == 0 && dbx == 0 || dax != 0 && dbx != 0)
      {
        return false;
      }
      bool testX, testY;
      if (dax == 0) // Line A is Vertical
      {
        testX = InRange(a1.x, b1.x, b2.x);
        testY = InRange(b1.y, a1.y, a2.y);
      }
      else // Line A is Horizontal
      {
        testX = InRange(b1.x, a1.x, a2.x);
        testY = InRange(a1.y, b1.y, b2.y);
      }
      return testX && testY;
    }

    private static Coord GetIntersectingCoord(Coord a1, Coord a2, Coord b1, Coord b2)
    {
      int dax = a1.x - a2.x; // Line A is Vertical
      if (dax == 0)
      {
        return new Coord(a1.x, b1.y);
      }
      else // Line B is Horizontal
      {
        return new Coord(b1.x, a1.y);
      }
    }

    private static Coord ClosestCoord(List<Coord> coords, out int manhatan)
    {
      Coord closest = new Coord(0, 0);
      int shortest = int.MaxValue;
      foreach (Coord c in coords)
      {
        // Ignore the intersection at the origin.
        if (c.x == 0 && c.y == 0)
        {
          continue;
        }
        // Get Absolute values
        int x = c.x > 0 ? c.x : -1 * c.x;
        int y = c.y > 0 ? c.y : -1 * c.y;
        int distance = x + y;
        if (distance < shortest)
        {
          closest = c;
          shortest = distance;
        }
      }
      manhatan = shortest;
      return closest;
    }
    // Check if a value is in a range, compensates for reversed min,max values
    private static bool InRange(int v, int a, int b)
    {
      if (a > b)
      {
        return v >= b && v <= a;
      }
      else
      {
        return v >= a && v <= b;
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
    public override string ToString()
    {
      return "X: " + x.ToString() + ", Y: " + y.ToString();
    }
  }
}
