using System;

namespace AdventOfCode2022
{
    public class Day3 : Day
    {
        public long Part1(string[] lines)
        {
            long sumOfpriority = 0;

            foreach (var line in lines)
            {
                int middlePos = line.Length / 2;
                var leftCompartments = line.Substring(0, middlePos);
                var rightCompartments = line.Substring(middlePos, middlePos);

                char commonItem = leftCompartments.Intersect(rightCompartments).ElementAt(0);

                int prority = CalculatePrority(commonItem);

                sumOfpriority += prority;
            }

            return sumOfpriority;
        }

        public long Part2(string[] lines)
        {
            long sumOfpriority = 0;

            for (int i = 0; i < lines.Length; i += 3)
            {
                char badge = lines[i].Intersect(lines[i + 1].Intersect(lines[i + 2])).ElementAt(0);

                int prority = CalculatePrority(badge);

                sumOfpriority += prority;
            }

            return sumOfpriority;
        }

        private int CalculatePrority(char commonItem)
        {
            int offset = 96;
            int bonus = 0;
            if (Char.IsUpper(commonItem))
            {
                offset = 64;
                bonus = 26;
            }

            int prority = (int)commonItem - offset + bonus;
            return prority;
        }
    }
}