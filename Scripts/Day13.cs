using System;
namespace AdventOfCode2022
{
    public class Day13 : Day
    {
        public long Part1(string[] lines)
        {
            Packet[] packets = new Packet[2];

            int sumGoodOrder = 0;
            string lastnumber = "";

            for (int i = 0; i < lines.Length; i++)
            {

                if (lines[i] == "")
                {

                    if (CompareTwoPackets(packets))
                    {
                        sumGoodOrder += (i / 3 + 1);
                        Console.WriteLine((i / 3 + 1) + ": Good Order");
                    }
                    else
                        Console.WriteLine((i / 3 + 1) + ": NOT Goot Order");

                    continue;
                }

                Packet? currentPackage = null;

                foreach (var chars in lines[i])
                {
                    if (chars == '[')
                    {
                        if (currentPackage == null)
                            currentPackage = new Packet();
                        else
                        {
                            Packet newPacket = new Packet();

                            if (currentPackage.Numbers.Count >= 1)
                            {
                                newPacket = new Packet();
                                currentPackage.Packets.Add(newPacket);
                                newPacket.Parent = currentPackage;
                                foreach (var number in currentPackage.Numbers)
                                    newPacket.Numbers.Add(number);

                                currentPackage.Numbers.Clear();
                            }

                            newPacket = new Packet();
                            currentPackage.Packets.Add(newPacket);
                            newPacket.Parent = currentPackage;
                            currentPackage = newPacket;
                        }
                    }
                    else if (chars == ']')
                    {
                        int numberToAdd;
                        if (int.TryParse(lastnumber, out numberToAdd))
                            currentPackage.Numbers.Add(numberToAdd);

                        lastnumber = "";

                        if (currentPackage.Parent != null)
                            currentPackage = currentPackage.Parent;
                    }
                    else if (chars == ',')
                    {
                        if (lastnumber == "")
                            continue;

                        currentPackage.Numbers.Add(int.Parse(lastnumber));
                        lastnumber = "";
                    }
                    else
                    {
                        lastnumber += chars;
                    }
                }

                packets[i % 3] = currentPackage;

            }

            return (long)sumGoodOrder;
        }

        private bool CompareTwoPackets(Packet[] packets)
        {

            while (packets[0] != null && packets[1] != null)
            {

                var deepestLeft = GetDeepest(packets[0], out packets[0]);
                var deepestRight = GetDeepest(packets[1], out packets[1]);

                for (int i = 0; i < Math.Min(deepestRight.Count, deepestLeft.Count); i++)
                {
                    if (deepestLeft[i] > deepestRight[i])
                    {
                        return false;
                    }
                    if (deepestLeft[i] < deepestRight[i])
                    {
                        return true;
                    }
                }
                if (deepestLeft.Count > deepestRight.Count)
                {
                    return false;
                }
                if (deepestLeft.Count < deepestRight.Count)
                {
                    return true;
                }
            }

            Console.WriteLine("?");
            return false;
        }

        private List<int> GetDeepest(Packet packet1, out Packet packet)
        {
            packet = packet1;

            var currentPacket = packet;
            while (currentPacket.Packets.Count != 0)
            {
                currentPacket = currentPacket.Packets[0];
            }

            var numbers = currentPacket.Numbers.ToList();

            if (currentPacket.Parent == null)
            {
                packet.Packets.Remove(currentPacket);

                if (packet.Packets.Count == 0)
                    packet = null;
            }
            else
            {
                Console.WriteLine(packet.Packets.Count);
                currentPacket.Parent.Packets.Remove(currentPacket);
            }

            return numbers;
        }

        public long Part2(string[] lines)
        {
            return -1;
        }
    }

    public class Packet
    {
        public Packet? Parent = null;
        public List<Packet> Packets = new List<Packet>();

        public List<int> Numbers = new List<int>();
    }
}