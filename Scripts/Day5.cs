using System;
namespace AdventOfCode2022
{
    public class Day5 : Day
    {
        const int STEP = 4;
        const int NUM_OF_STACK = 9;

        LinkedList<char>[] stacks = new LinkedList<char>[NUM_OF_STACK];

        public long Part1(string[] lines)
        {
            DoRearrangement(lines);

            PrintResult();
            return 0;
        }
        public long Part2(string[] lines)
        {
            DoRearrangement(lines, true);

            PrintResult();
            return 0;
        }


        private void DoRearrangement(string[] lines, bool crateMover9001 = false)
        {
            for (int i = 0; i < NUM_OF_STACK; i++)
                stacks[i] = new LinkedList<char>();


            bool startMoveInput = false;
            foreach (var line in lines)
            {
                if (line.Length <= 1)
                {
                    startMoveInput = true;
                    continue;
                }

                if (!startMoveInput)
                    AddCreateToStack(line);
                else
                    MoveCreatesFromToStack(crateMover9001, line);
            }
        }

        private void AddCreateToStack(string line)
        {
            for (int i = 0; i < NUM_OF_STACK; i++)
            {
                char create = line.Substring(1 + (STEP * i), 1)[0];

                if (char.IsLetter(create))
                    stacks[i].AddFirst(create);
            }
        }
        private void MoveCreatesFromToStack(bool crateMover9001, string line)
        {
            var moveCreateInfo = line.Split(' ');
            int howManyCreates = int.Parse(moveCreateInfo[1]);
            int fromStack = int.Parse(moveCreateInfo[3]) - 1;
            int toStack = int.Parse(moveCreateInfo[5]) - 1;

            string movingCreates = "";

            for (int i = 0; i < howManyCreates; i++)
            {
                var create = stacks[fromStack].Last;
                stacks[fromStack].RemoveLast();
                movingCreates += create.Value;

                if (i == howManyCreates - 1)
                {
                    if (crateMover9001)
                        movingCreates = Revert(movingCreates);

                    foreach (var _create in movingCreates)
                        stacks[toStack].AddLast(_create);
                }
            }
        }

        private string Revert(string movingCreates) => new string(movingCreates.ToCharArray().Reverse().ToArray());

        private void PrintResult()
        {
            string result = "";
            for (int i = 0; i < NUM_OF_STACK; i++)
            {
                if (stacks[i].Count == 0)
                    continue;

                result += stacks[i].Last();
            }
            Console.WriteLine(result);
        }
    }
}