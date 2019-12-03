using System.Linq;

namespace DayTwo
{
  class IntComputer
  {
    private int[] program;
    public int ProgramSize
    {
      get
      {
        return program.Length;
      }
    }
    private int[] memory;
    public IntComputer(string _program)
    {
      program = _program.Split(',').Select(str => int.Parse(str)).ToArray();
      memory = new int[program.Length];
    }
    public int Run()
    {
      ResetMemory();
      Process();
      return memory[0];
    }
    public int Run(int pos1, int pos2)
    {
      ResetMemory();
      SetMemory(pos1, 1);
      SetMemory(pos2, 2);
      Process();
      return memory[0];
    }
    public void SetMemory(int value, int location)
    {
      memory[location] = value;
    }
    private void Process()
    {
      int cursor = 0;
      while (memory[cursor] != 99)
      {
        switch (memory[cursor])
        {
          case 1:
            OpAdd(memory[cursor + 1], memory[cursor + 2], memory[cursor + 3]);
            break;
          case 2:
            OpMultiply(memory[cursor + 1], memory[cursor + 2], memory[cursor + 3]);
            break;
        }
        cursor += 4;
      }
    }
    private void ResetMemory()
    {
      program.CopyTo(memory, 0);
    }
    private void OpAdd(int a, int b, int loc)
    {
      memory[loc] = memory[a] + memory[b];
    }
    private void OpMultiply(int a, int b, int loc)
    {
      memory[loc] = memory[a] * memory[b];
    }
    private void OpSubtract(int a, int b, int loc)
    {
      memory[loc] = memory[a] - memory[b];
    }
    private void OpDivide(int a, int b, int loc)
    {
      memory[loc] = memory[a] / memory[b];
    }
  }
}