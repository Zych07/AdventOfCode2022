using System;
namespace AdventOfCode2022
{
    public class Day6 : Day
    {
        public long Part1(string[] lines)
        {
            int howManyProcessedCharacters = ProccesPacket(lines, 4);

            return howManyProcessedCharacters;
        }
        public long Part2(string[] lines)
        {
            int howManyProcessedCharacters = ProccesPacket(lines, 14);

            return howManyProcessedCharacters;
        }

        private int ProccesPacket(string[] lines, int sequenceLength)
        {
            Queue<char> lastFourCharacters = new Queue<char>();

            int charactersProcessed = 0;

            foreach (var line in lines)
            {
                foreach (char character in line)
                {
                    if (lastFourCharacters.Count == sequenceLength)
                    {
                        bool anyDuplicate = lastFourCharacters.GroupBy(x => x).Any(y => y.Count() > 1);

                        if (!anyDuplicate)
                            return charactersProcessed;

                        lastFourCharacters.Dequeue();
                    }

                    lastFourCharacters.Enqueue(character);
                    charactersProcessed++;
                }
            }
            return -1;
        }
    }
}