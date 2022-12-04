using System;

namespace AdventOfCode2022
{
    public class Day4 : Day
    {
        public long Part1(string[] lines)
        {
            int fullyContainsPairs = 0;
            foreach (var line in lines)
            {
                (int, int) rangeLeft, rangeRight;
                ParseInput(line, out rangeLeft, out rangeRight);
                
                if (rangeLeft.Item1 >= rangeRight.Item1 && rangeLeft.Item2 <= rangeRight.Item2)
                    fullyContainsPairs++;
                else if (rangeRight.Item1 >= rangeLeft.Item1 && rangeRight.Item2 <= rangeLeft.Item2)
                    fullyContainsPairs++;
            }

            return fullyContainsPairs;
        }

        public long Part2(string[] lines)
        {
            int overlapPairs = 0;
            foreach (var line in lines)
            {
                (int, int) rangeLeft, rangeRight;
                ParseInput(line, out rangeLeft, out rangeRight);

                if (rangeLeft.Item1 <= rangeRight.Item2 && rangeLeft.Item1 >= rangeRight.Item1)
                    overlapPairs++;
                else if (rangeLeft.Item2 <= rangeRight.Item2 && rangeLeft.Item2 >= rangeRight.Item1)
                    overlapPairs++;
                else if (rangeRight.Item1 <= rangeLeft.Item2 && rangeRight.Item1 >= rangeLeft.Item1)
                    overlapPairs++;
                else if (rangeRight.Item2 <= rangeLeft.Item2 && rangeRight.Item2 >= rangeLeft.Item1)
                    overlapPairs++;
            }

            return overlapPairs;
        }

        private static void ParseInput(string line, out (int, int) rangeLeft, out (int, int) rangeRight)
        {
            var pairs = line.Split(',');
            var leftElf = pairs[0].Split('-');
            var rightElf = pairs[1].Split('-');

            rangeLeft = (int.Parse(leftElf[0]), int.Parse(leftElf[1]));
            rangeRight = (int.Parse(rightElf[0]), int.Parse(rightElf[1]));
        }
    }
}