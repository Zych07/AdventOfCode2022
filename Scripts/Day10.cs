using System;
namespace AdventOfCode2022
{
    public class Day10 : Day
    {

        private int _currentCycle = 0;

        public long Part1(string[] lines)
        {
            long sumOfSignals = 0;
            int X = 1;

            ResetClock();

            foreach (var line in lines)
            {
                sumOfSignals += DoCycle(X);

                if (line == "noop")
                    continue;
                else
                {
                    sumOfSignals += DoCycle(X);

                    X += int.Parse(line.Split(' ')[1].ToString());
                }
            }

            Console.WriteLine();
            return sumOfSignals;
        }

        public long Part2(string[] lines)
        {
            //Solved simultaneously with Part1
            return 0;
        }


        private int DoCycle(int X)
        {
            DrawPixel(X);

            _currentCycle++;

            if ((_currentCycle + 20) % 40 == 0)
                return _currentCycle * X;

            return 0;
        }

        private void DrawPixel(int X)
        {
            if (_currentCycle % 40 == 0)
                Console.WriteLine();

            if (Math.Abs(_currentCycle % 40 - X) <= 1)
                Console.Write("#");
            else
                Console.Write(" ");
        }

        private void ResetClock() => _currentCycle = 0;
    }
}