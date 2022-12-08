using System;
namespace AdventOfCode2022
{
    public class Day7 : Day
    {
        const long TOTAL_DISK_SPACE = 70000000;
        const long UPGRADE_SIZE = 30000000;

        public long Part1(string[] lines)
        {
            Dic root = ParseInput(lines);

            return GetSumDictionaries(root);
        }

        public long Part2(string[] lines)
        {
            Dic root = ParseInput(lines);

            long freeSpace = TOTAL_DISK_SPACE - root.TotalSize;
            long minFileToDelete = UPGRADE_SIZE - freeSpace;

            return GetMinSize(root, minFileToDelete);
        }

        private long GetSumDictionaries(Dic root, long limitSize = 100000)
        {
            long size = 0;

            foreach (var child in root.Children)
                size += GetSumDictionaries(child, limitSize);

            size += (root.TotalSize <= limitSize ? root.TotalSize : 0);

            return size;
        }

        private long GetMinSize(Dic root, long minFileSize)
        {
            long minSize = long.MaxValue;

            foreach (var child in root.Children)
                minSize = Math.Min(GetMinSize(child, minFileSize), minSize);

            if (root.TotalSize >= minFileSize)
                minSize = Math.Min(root.TotalSize, minSize);

            return minSize;
        }

        private Dic ParseInput(string[] lines)
        {
            Dic root = new Dic("/", null);

            Dic currentDic = root;

            foreach (var line in lines)
            {
                if (line[0] == '$')
                {
                    var param = line.Split(' ');

                    if (param[1] == "cd")
                    {
                        switch (param[2])
                        {
                            case "/":
                                currentDic = root;
                                break;
                            case "..":
                                currentDic = currentDic.Parent;
                                break;
                            default:
                                currentDic = currentDic.Children.First(x => x.Name == param[2]);
                                break;
                        }
                    }
                }
                else
                {
                    var param = line.Split(' ');

                    if (param[0] == "dir")
                        currentDic.CreateDic(param[1]);
                    else
                        currentDic.CreateFile(param[1], long.Parse(param[0]));
                }
            }

            root.CalculateSize();

            return root;
        }

        private class Dic
        {
            public string Name = "";
            public Dic Parent;
            public List<Dic> Children = new List<Dic>();
            public List<File> Files = new List<File>();

            public long TotalSize { get; private set; }

            public Dic(string name, Dic parent)
            {
                Name = name;
                Parent = parent;
            }

            public void CreateFile(string name, long size)
            {
                File file = new File(name, size);
                Files.Add(file);
            }

            public void CreateDic(string name)
            {
                Dic dic = new Dic(name, this);
                Children.Add(dic);
            }

            public void CalculateSize()
            {
                long totalSize = 0;

                foreach (var child in Children)
                    child.CalculateSize();

                totalSize += Children.Sum(x => x.TotalSize);
                totalSize += Files.Sum(x => x.Size);

                TotalSize = totalSize;
            }
        }

        private class File
        {
            public string Name = "";
            public long Size;

            public File(string name, long size)
            {
                Name = name;
                Size = size;
            }
        }

    }
}