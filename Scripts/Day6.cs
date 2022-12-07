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
            Queue<char> charactersSqeuence = new Queue<char>();

            int charactersProcessed = 0;

            foreach (var line in lines)
            {
                foreach (char character in line)
                {
                    if (charactersSqeuence.Count == sequenceLength)
                    {
                        bool anyDuplicate = charactersSqeuence.GroupBy(x => x).Any(y => y.Count() > 1);

                        if (!anyDuplicate)
                            return charactersProcessed;

                        charactersSqeuence.Dequeue();
                    }

                    charactersSqeuence.Enqueue(character);
                    charactersProcessed++;
                }
            }
            return -1;
        }
    }
}