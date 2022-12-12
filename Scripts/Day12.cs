using System;
namespace AdventOfCode2022
{
    public class Day12 : Day
    {
        private (int, int)[] AdjDirections = { (0, 1), (0, -1), (1, 0), (-1, 0) };
        
        private int sizeX;
        private int sizeY;

        private int[,] Array;
        private (int, int) ExpeditionStart;
        private (int, int) HighestPeek;

        private List<(int, int)> Targets = new List<(int, int)>();


        public long Part1(string[] lines)
        {
            InitInput(lines);

            Targets.Add(ExpeditionStart);
            BFS(HighestPeek);

            return BFS(HighestPeek);
        }


        public long Part2(string[] lines)
        {
            InitInput(lines);

            for (int i = 0; i < sizeY; i++)
                for (int j = 0; j < sizeX; j++)
                    if (Array[i, j] == 0)
                        Targets.Add((i, j));

            BFS(HighestPeek);

            return BFS(HighestPeek);
        }

        private int BFS((int, int) start)
        {
            Queue<(int, int)> toVisitNode = new Queue<(int, int)>();
            int[,] distance = new int[sizeY, sizeX];
            bool[,] visited = new bool[sizeY, sizeX];

            toVisitNode.Enqueue(start);
            visited[start.Item1, start.Item2] = true;

            while (toVisitNode.Count != 0)
            {
                (int, int) current = toVisitNode.Dequeue();

                for (int i = 0; i < AdjDirections.Length; i++)
                {
                    if (current.Item1 + AdjDirections[i].Item1 >= 0 && current.Item1 + AdjDirections[i].Item1 < sizeY && current.Item2 + AdjDirections[i].Item2 >= 0 && current.Item2 + AdjDirections[i].Item2 < sizeX)
                    {
                        (int, int) adj = (current.Item1 + AdjDirections[i].Item1, current.Item2 + AdjDirections[i].Item2);

                        if (visited[adj.Item1, adj.Item2] == false && IsProperHeight(current, adj))
                        {
                            visited[adj.Item1, adj.Item2] = true;
                            toVisitNode.Enqueue((adj.Item1, adj.Item2));
                            distance[adj.Item1, adj.Item2] = distance[current.Item1, current.Item2] + 1;
                            if (Targets.Contains((adj.Item1, adj.Item2)))
                                return distance[adj.Item1, adj.Item2];
                        }
                    }
                }
            }
            return -1;
        }

        private bool IsProperHeight((int, int) current, (int, int) next) => Math.Abs(Array[current.Item1, current.Item2] - Array[next.Item1, next.Item2]) <= 1 || Array[current.Item1, current.Item2] < Array[next.Item1, next.Item2];

        private void InitInput(string[] lines)
        {
            sizeY = lines.Length;
            sizeX = lines[0].Length;

            Array = new int[sizeY, sizeX];
            Targets.Clear();

            for (int i = 0; i < sizeY; i++)
            {
                for (int j = 0; j < sizeX; j++)
                {
                    if (lines[i].ElementAt(j) == 'S')
                    {
                        Array[i, j] = (int)'a' - 97;
                        ExpeditionStart = (i, j);
                    }
                    else if (lines[i].ElementAt(j) == 'E')
                    {
                        Array[i, j] = (int)'z' - 97;
                        HighestPeek = (i, j);
                    }
                    else
                    {
                        Array[i, j] = (int)lines[i].ElementAt(j) - 97;
                    }
                }
            }
        }
    }
}