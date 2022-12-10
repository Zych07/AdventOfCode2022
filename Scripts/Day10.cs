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
            return sumOfSignals;
        }

        public long Part2(string[] lines)
        {
            return -1;
        }


        private int DoCycle(int X)
        {
            _currentCycle++;

            if ((_currentCycle + 20) % 40 == 0)
                return _currentCycle * X;

            return 0;
        }
        private void ResetClock() => _currentCycle = 0;
    }
}