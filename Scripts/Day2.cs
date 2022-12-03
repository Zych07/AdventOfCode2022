using System;

namespace AdventOfCode2022
{
    public class Day2 : Day
    {
        Dictionary<string, int> points = new Dictionary<string, int>()
        {
            {"X", 1},
            {"Y", 2},
            {"Z", 3},
        };

        Dictionary<string, int> results = new Dictionary<string, int>()
        {
            {"C Y", 0},
            {"A Z", 0},
            {"B X", 0},

            {"A X", 3},
            {"B Y", 3},
            {"C Z", 3},

            {"A Y", 6},
            {"B Z", 6},
            {"C X", 6},
        };

        public long Part1(string[] lines)
        {
            long sumOfScore = 0;
            foreach (var line in lines)
                sumOfScore += points[line.Split(' ')[1]] + results[line];

            return sumOfScore;
        }

        public long Part2(string[] lines)
        {
            long sumOfScore = 0;

            foreach (var line in lines)
            {
                string[] input = line.Split(" ");
                int valueOfResult = (points[input[1]] - 1) * 3;

                var result = results.First(x => x.Key[0] == input[0][0] && x.Value == valueOfResult);

                sumOfScore += points[result.Key[2].ToString()];
                sumOfScore += valueOfResult;
            }

            return sumOfScore;
        }
    }
}