using System;
namespace AdventOfCode2022
{
    public class Day9 : Day
    {
        private Dictionary<string, (int, int)> _directions = new Dictionary<string, (int, int)>()
        {
            {"U", (0,1)},
            {"D", (0,-1)},
            {"R", (1,0)},
            {"L", (-1,0)},
        };

        private List<(int, int)> _visitedPos = new List<(int, int)>();
        private (int, int)[] _rope;


        public long Part1(string[] lines)
        {
            _rope = new (int, int)[2];

            RopeMotions(lines);

            return _visitedPos.Count;
        }
        public long Part2(string[] lines)
        {
            _rope = new (int, int)[10];

            RopeMotions(lines);

            return _visitedPos.Count;
        }


        private void RopeMotions(string[] lines)
        {
            _visitedPos.Clear();
            
            foreach (var line in lines)
            {
                var input = line.Split(" ");
                int steps = int.Parse(input[1]);

                for (int i = 0; i < steps; i++)
                    DoStep(_directions[input[0]]);
            }
        }

        private void DoStep((int, int) direction)
        {
            _rope[0] = (direction.Item1 + _rope[0].Item1, direction.Item2 + _rope[0].Item2);

            for (int i = 1; i < _rope.Length; i++)
            {
                (int, int) header = _rope[i - 1];
                (int, int) tail = _rope[i];

                if (!DistanceBetweenKnotsIsTooFar(header, tail))
                    break;

                (int, int) normalizedDirection = (header.Item1 - tail.Item1, header.Item2 - tail.Item2);

                normalizedDirection.Item1 = Math.Clamp(normalizedDirection.Item1, -1, 1);
                normalizedDirection.Item2 = Math.Clamp(normalizedDirection.Item2, -1, 1);

                tail = (normalizedDirection.Item1 + tail.Item1, normalizedDirection.Item2 + tail.Item2);

                _rope[i - 1] = header;
                _rope[i] = tail;
            }

            if (!_visitedPos.Contains((_rope[_rope.Length - 1].Item1, _rope[_rope.Length - 1].Item2)))
                _visitedPos.Add((_rope[_rope.Length - 1].Item1, _rope[_rope.Length - 1].Item2));

        }

        private bool DistanceBetweenKnotsIsTooFar((int, int) head, (int, int) tail)
        {
            if (Math.Abs(head.Item1 - tail.Item1) >= 2)
                return true;
            if (Math.Abs(head.Item2 - tail.Item2) >= 2)
                return true;

            return false;
        }
    }
}