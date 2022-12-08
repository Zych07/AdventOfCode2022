using System;
namespace AdventOfCode2022
{
    public class Day8 : Day
    {
        private int[,] forest;
        public long Part1(string[] lines)
        {
            ParesInput(lines);

            int countVisible = (forest.GetLength(0) * 2) + (forest.GetLength(1) * 2) - 4;

            for (int i = 1; i < forest.GetLength(0) - 1; i++)
                for (int j = 1; j < forest.GetLength(1) - 1; j++)
                    if (IsVisible(i, j))
                        countVisible++;

            return countVisible;

        }

        public long Part2(string[] lines)
        {
            ParesInput(lines);

            int maxViewRange = int.MinValue;

            for (int i = 1; i < forest.GetLength(0) - 1; i++)
                for (int j = 1; j < forest.GetLength(1) - 1; j++)
                    maxViewRange = Math.Max(GetViewRange(i, j), maxViewRange);

            return maxViewRange;

        }

        private int GetViewRange(int i, int j)
        {
            int viewRange;
            int distanceView = 0;
            for (int k = j - 1; k >= 0; k--)
            {
                distanceView++;
                if (forest[i, k] >= forest[i, j])
                    break;
            }
            viewRange = distanceView;
            distanceView = 0;

            for (int k = j + 1; k < forest.GetLength(1); k++)
            {
                distanceView++;
                if (forest[i, k] >= forest[i, j])
                    break;
            }
            viewRange *= distanceView;
            distanceView = 0;

            for (int k = i - 1; k >= 0; k--)
            {
                distanceView++;
                if (forest[k, j] >= forest[i, j])
                    break;
            }
            viewRange *= distanceView;
            distanceView = 0;

            for (int k = i + 1; k < forest.GetLength(0); k++)
            {
                distanceView++;
                if (forest[k, j] >= forest[i, j])
                    break;
            }
            viewRange *= distanceView;
            return viewRange;
        }

        private bool IsVisible(int i, int j)
        {
            bool isVisible = true;

            for (int k = 0; k < j; k++)
                if (forest[i, k] >= forest[i, j])
                    isVisible = false;
            if (isVisible)
                return true;

            isVisible = true;
            for (int k = j + 1; k < forest.GetLength(1); k++)
                if (forest[i, k] >= forest[i, j])
                    isVisible = false;
            if (isVisible)
                return true;

            isVisible = true;
            for (int k = 0; k < i; k++)
                if (forest[k, j] >= forest[i, j])
                    isVisible = false;
            if (isVisible)
                return true;

            isVisible = true;
            for (int k = i + 1; k < forest.GetLength(0); k++)
                if (forest[k, j] >= forest[i, j])
                    isVisible = false;
            if (isVisible)
                return true;

            return false;
        }

        private void ParesInput(string[] lines)
        {
            forest = new int[lines.Length, lines[0].Length];

            for (int i = 0; i < forest.GetLength(0); i++)
                for (int j = 0; j < forest.GetLength(1); j++)
                    forest[i, j] = int.Parse(lines[i][j].ToString());
        }
    }
}