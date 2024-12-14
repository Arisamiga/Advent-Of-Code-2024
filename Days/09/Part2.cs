namespace _09
{
    // Applied by Part1.cs
    //public class ContentFile
    //{
    //    public int id { get; set; }
    //    public ulong Block { get; set; }
    //    public ulong? FreeSpace { get; set; }
    //}

    internal class Part2
    {
        public static List<ContentFile> FormatDiskmap(string diskmap)
        {
            var fileblocks = new List<ContentFile>();
            for (int i = 0, id = 0; i < diskmap.Length; i++)
            {
                var digit = int.Parse($"{diskmap[i]}");
                if (i % 2 == 1)
                {
                    for (var j = 0; j < digit; j++)
                        fileblocks.Add(new ContentFile { id = -1, FreeSpace = 1 }); // -1 represents free space
                }
                else
                {
                    for (var j = 0; j < digit; j++)
                        fileblocks.Add(new ContentFile { id = id, Block = (ulong)id });
                    id++;
                }
            }
            return fileblocks;
        }
        public static List<ContentFile> SortDiskmap(List<ContentFile> fileblocks)
        {
            // Precompute the count of each id
            var idCounts = fileblocks
                .Where(fb => fb.id >= 0)
                .GroupBy(fb => fb.id)
                .ToDictionary(g => g.Key, g => g.Count());

            for (var i = fileblocks.Count - 1; i >= 0; i--)
            {
                if (fileblocks[i].id >= 0)
                {
                    int id = fileblocks[i].id;
                    ulong requiredspace = (ulong)idCounts[id];
                    var spaces = FindIndexFree(fileblocks, i, requiredspace);

                    if (spaces[0] < 0)
                        continue;

                    for (var j = 0; j < (int)requiredspace; j++)
                    {
                        fileblocks[spaces[0] + j] = fileblocks[i - j];
                        fileblocks[i - j] = new ContentFile { id = -1, FreeSpace = 1 };
                    }

                    i -= (int)requiredspace - 1; // Adjust the index to skip over the moved blocks
                }
            }
            return fileblocks;
        }

        public static int[] FindIndexFree(List<ContentFile> fileblocks, int maxIndex, ulong requiredspace)
        {
            int spaces = 0;
            int startOfSpaces = -1;

            for (var i = 0; i < maxIndex; i++)
            {
                if (fileblocks[i].id == -1)
                {
                    if (spaces == 0)
                        startOfSpaces = i;
                    spaces++;
                    if ((ulong)spaces >= requiredspace)
                        return new int[] { startOfSpaces, spaces };
                }
                else
                {
                    spaces = 0;
                    startOfSpaces = -1;
                }
            }
            return new int[] { -1, 0 };
        }

        public static long CalculateChecksum(List<ContentFile> fileblocks)
        {
            long checksum = 0;
            for (var i = 0; i < fileblocks.Count; i++)
            {
                if (fileblocks[i].id < 0)
                {
                    continue;
                }
                checksum += fileblocks[i].id * i;
            }

            return checksum;
        }

        static void Main(string[] args)
        {
            string diskmap = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "value.txt"));

            List<ContentFile> reformattedDiskmap = FormatDiskmap(diskmap);
            List<ContentFile> compactedDiskmap = SortDiskmap(reformattedDiskmap);
            long checksum = CalculateChecksum(compactedDiskmap);

            Console.WriteLine(checksum);
        }
    }
}