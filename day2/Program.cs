using System;

namespace DayTwo
{
    class IntCode
    {
        static void Main(string[] args)
        {
            int[] program = new int[] {1,12,2,3,1,1,2,3,1,3,4,3,1,5,0,3,2,10,1,19,1,19,6,23,2,13,23,27,1,27,13,31,1,9,31,35,1,35,9,39,1,39,5,43,2,6,43,47,1,47,6,51,2,51,9,55,2,55,13,59,1,59,6,63,1,10,63,67,2,67,9,71,2,6,71,75,1,75,5,79,2,79,10,83,1,5,83,87,2,9,87,91,1,5,91,95,2,13,95,99,1,99,10,103,1,103,2,107,1,107,6,0,99,2,14,0,0};
            
            int[] answer = FindOutput(program, 19690720);
            if(answer == null)
            {
                Console.WriteLine("No Solution Found");
            }
            else
            {
                Console.WriteLine("Noun: " + answer[0].ToString() + ", Verb: " + answer[1].ToString());
            }
        }

        static int Process(int[] memory) {
            int cursor = 0;
            while(cursor < memory.Length)
            {
                int a = memory[cursor + 1];
                int b = memory[cursor + 2];
                int location = memory[cursor + 3];

                if(memory[cursor] == 1) 
                {
                    memory[location] = memory[a] + memory[b];
                }
                else if(memory[cursor] == 2) 
                {
                    memory[location] = memory[a] * memory[b];
                }
                else if(memory[cursor] == 99) 
                {
                    break;
                }
                    cursor += 4;
            }
            return memory[0];
        }

        static int[] FindOutput(int[] memory, int goal) 
        {
            for(int i = 0; i < memory.Length; i++) 
            {
                for(int j = 0; j < memory.Length; j++) 
                {
                    int[] test = new int[memory.Length];
                    Array.Copy(memory,test,memory.Length);
                    test[1] = i;
                    test[2] = j;
                    if(Process(test) == goal)
                    {
                        return new int[] {i,j};
                    }
                }
            }
            // No match found.
            return null;
        }
    }
}
    