using System;
namespace AdventOfCode2022
{
    public class Day11 : Day
    {
        
        const int MAX_ROUND = 20;
        private static List<Monkey> _monkeys = new List<Monkey>();
        public long Part1(string[] lines)
        {
            ParseInput(lines);

            for (int i = 0; i < MAX_ROUND; i++)
            {
                foreach (var monkey in _monkeys)
                    monkey.MakeTurn();
            }

            foreach (var monkey in _monkeys)
                Console.WriteLine("Monkey " + monkey.ID + " inspected items " + monkey.InspectedItem + " times.");

            var monekysWithMostInspectedItems = _monkeys.OrderByDescending(x => x.InspectedItem).Select(x => x.InspectedItem).Take(2);

            return (long)monekysWithMostInspectedItems.Aggregate((x, y) => x * y);
        }

        public long Part2(string[] lines)
        {
            return -1;
        }

        private void ParseInput(string[] lines)
        {
            _monkeys.Clear();

            Monkey? currentMonkey = null;

            foreach (var line in lines)
            {
                if (line.Length <= 1)
                    _monkeys.Add(currentMonkey);
                else if (line.Contains("Monkey"))
                    currentMonkey = new Monkey(int.Parse(line.Replace(':', ' ').Split(' ')[1]));
                else if (line.Contains("Operation"))
                    currentMonkey.Calculactions = line.Split("= ")[1];
                else if (line.Contains("Test"))
                    currentMonkey.Divisible = int.Parse(line.Split("by ")[1]);
                else if (line.Contains("If true"))
                    currentMonkey.PassToMonekyWhenTrue = int.Parse(line.Split("monkey ")[1]);
                else if (line.Contains("If false"))
                    currentMonkey.PassToMonekyWhenFalse = int.Parse(line.Split("monkey ")[1]);
                else if (line.Contains("Starting items:"))
                {
                    var editedLine = line.Replace("  Starting items:", ",");
                    var items = editedLine.Split(", ");

                    foreach (var item in items)
                    {
                        if (item == string.Empty)
                            continue;
                        currentMonkey.items.Enqueue(UInt64.Parse(item));
                    }
                }
            }
        }

        private class Monkey
        {
            public int ID;
            public Queue<UInt64> items = new Queue<UInt64>();

            public int Divisible;
            public int PassToMonekyWhenTrue;
            public int PassToMonekyWhenFalse;

            public string Calculactions = "";

            public UInt64 InspectedItem { get; private set; }

            public Monkey(int id) => ID = id;

            public void MakeTurn()
            {
                foreach (var item in items)
                {
                    InspectedItem++;

                    UInt64 worryLevel = item;
                    worryLevel = Operation(worryLevel);

                    worryLevel /= 3;

                    int monekyToPassItem = PassToMonekyWhenFalse;
                    if (worryLevel % (UInt64)Divisible == 0)
                        monekyToPassItem = PassToMonekyWhenTrue;

                    _monkeys.First(x => x.ID == monekyToPassItem).items.Enqueue(worryLevel);
                }
                items.Clear();
            }

            private UInt64 Operation(UInt64 worryLevel)
            {
                UInt64[] factors = new UInt64[2];
                var splitedEquation = Calculactions.Split(' ');
                SetFactors(worryLevel, factors, splitedEquation);

                switch (splitedEquation[1])
                {
                    case "+":
                        return factors[0] + factors[1];
                    case "*":
                        return factors[0] * factors[1];
                    default:
                        throw new Exception();
                }
            }

            private static void SetFactors(UInt64 worryLevel, UInt64[] factors, string[] splitedEquation)
            {
                for (int i = 0; i < 2; i++)
                {
                    UInt64 factor;

                    if (splitedEquation[i * 2] == "old")
                        factor = worryLevel;
                    else
                        factor = UInt64.Parse(splitedEquation[i * 2]);

                    factors[i] = factor;
                }
            }
        }
    }
}