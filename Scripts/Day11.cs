using System;
namespace AdventOfCode2022
{
    public class Day11 : Day
    {

        private static Dictionary<int, int> _primeNumbers = new Dictionary<int, int>();

        private static List<Monkey> _monkeys = new List<Monkey>();
        public long Part1(string[] lines)
        {
            ParseInput(lines);

            int MAX_ROUND = 20;

            for (int i = 0; i < MAX_ROUND; i++)
                foreach (var monkey in _monkeys)
                    monkey.MakeTurnInt();

            return (long)_monkeys.OrderByDescending(x => x.InspectedItem).Select(x => x.InspectedItem).Take(2).Aggregate((x, y) => x * y);
        }

        public long Part2(string[] lines)
        {
            ParseInput(lines);

            int MAX_ROUND = 10000;

            for (int i = 0; i < MAX_ROUND; i++)
                foreach (var monkey in _monkeys)
                    monkey.MakeTurn();

            return (long)_monkeys.OrderByDescending(x => x.InspectedItem).Select(x => x.InspectedItem).Take(2).Aggregate((x, y) => x * y);
        }

        private void ParseInput(string[] lines)
        {
            _primeNumbers.Clear();
            _monkeys.Clear();

            Monkey? currentMonkey = null;

            int primeID = 0;

            foreach (var line in lines)
            {
                if (line.IndexOf("divisible by ") > 0)
                {
                    var primeNumber = line.Substring(line.IndexOf("divisible by ") + "divisible by ".Length);
                    _primeNumbers.Add(primeID, int.Parse(primeNumber));
                    primeID++;
                }

            }

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
                        currentMonkey.items.Enqueue(new Item(int.Parse(item)));
                        currentMonkey.itemsInt.Enqueue(int.Parse(item));
                    }
                }
            }
        }

        private class Monkey
        {
            public int ID;
            public Queue<Item> items = new Queue<Item>();
            public Queue<int> itemsInt = new Queue<int>();

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

                    Item worryLevel = item;
                    worryLevel = Operation(worryLevel);

                    int monekyToPassItem = PassToMonekyWhenFalse;
                    if (worryLevel.rest[_primeNumbers.First(x => x.Value == Divisible).Key] == 0)
                        monekyToPassItem = PassToMonekyWhenTrue;

                    _monkeys.First(x => x.ID == monekyToPassItem).items.Enqueue(worryLevel);
                }
                items.Clear();
            }

            public void MakeTurnInt()
            {
                foreach (var item in itemsInt)
                {
                    InspectedItem++;

                    int worryLevel = item;
                    worryLevel = Operation(worryLevel);

                    worryLevel /= 3;

                    int monekyToPassItem = PassToMonekyWhenFalse;
                    if (worryLevel % Divisible == 0)
                        monekyToPassItem = PassToMonekyWhenTrue;

                    _monkeys.First(x => x.ID == monekyToPassItem).itemsInt.Enqueue(worryLevel);
                }
                itemsInt.Clear();
            }

            private Item Operation(Item worryLevel)
            {
                Item[] factors = new Item[2];
                var splitedEquation = Calculactions.Split(' ');
                SetFactors(worryLevel, factors, splitedEquation);

                switch (splitedEquation[1])
                {
                    case "+":
                        return new Item(factors[0].Add(factors[1]));
                    case "*":
                        return new Item(factors[0].Multiply(factors[1]));
                    default:
                        throw new Exception();
                }
            }

            private int Operation(int worryLevel)
            {
                int[] factors = new int[2];
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

            private void SetFactors(Item worryLevel, Item[] factors, string[] splitedEquation)
            {
                for (int i = 0; i < 2; i++)
                {
                    Item factor;

                    if (splitedEquation[i * 2] == "old")
                        factor = new Item(worryLevel);
                    else
                        factor = new Item(int.Parse(splitedEquation[i * 2]));

                    factors[i] = factor;
                }
            }
            private void SetFactors(int worryLevel, int[] factors, string[] splitedEquation)
            {
                for (int i = 0; i < 2; i++)
                {
                    int factor;

                    if (splitedEquation[i * 2] == "old")
                        factor = worryLevel;
                    else
                        factor = int.Parse(splitedEquation[i * 2]);

                    factors[i] = factor;
                }
            }
        }

        private class Item
        {
            public int[] rest = new int[_primeNumbers.Count];


            public Item(int itemValue)
            {
                for (int i = 0; i < rest.Length; i++)
                    rest[i] = itemValue % _primeNumbers[i];
            }
            public Item(Item item)
            {
                for (int i = 0; i < rest.Length; i++)
                    rest[i] = item.rest[i];
            }

            public Item Add(Item value)
            {
                for (int i = 0; i < rest.Length; i++)
                {
                    rest[i] = (rest[i] + value.rest[i]) % _primeNumbers[i];
                }
                return this;

            }
            public Item Multiply(Item item)
            {
                for (int i = 0; i < rest.Length; i++)
                {
                    rest[i] = (rest[i] * item.rest[i]) % _primeNumbers[i];
                }

                return this;
            }
        }
    }
}