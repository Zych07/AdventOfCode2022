using System;

namespace AdventOfCode2022
{
    public class Day1 : Day
    {
        List<long> calloriesPerElf = new List<long>();

        public long Part1(string[] lines)
        {
            CalculateCallories(lines);

            return calloriesPerElf.Max();
        }

        public long Part2(string[] lines)
        {
            CalculateCallories(lines);

            long sumOfTopThree = calloriesPerElf.OrderByDescending(x => x).Take(3).Sum();

            return sumOfTopThree;
        }

        private void CalculateCallories(string[] lines)
        {
            calloriesPerElf.Clear();
            
            long sumOfcallories = 0;

            foreach (var line in lines)
            {
                if (line == "")
                {
                    calloriesPerElf.Add(sumOfcallories);
                    sumOfcallories = 0;
                }
                else
                    sumOfcallories += long.Parse(line);
            }
        }
    }
}