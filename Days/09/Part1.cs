namespace _09
{
    public class ContentFile
    {
        public int id { get; set; }
        public ulong Block { get; set; }
        public ulong? FreeSpace { get; set; }
    }

    internal class Part1
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
            int freespace = 0;
            for (var i = fileblocks.Count - 1; i >= 0; i--)
            {
                if (fileblocks[i].id >= 0)
                {
                    freespace = FindIndexFree(fileblocks, freespace, i);
                    if (freespace < 0)
                        break;

                    fileblocks[freespace] = fileblocks[i];
                    fileblocks[i] = new ContentFile { id = -1, FreeSpace = 1 };
                }
            }
            return fileblocks;
        }

        public static int FindIndexFree(List<ContentFile> fileblocks, int start, int maxIndex)
        {
            for (var i = start; i < maxIndex; i++)
                if (fileblocks[i].id == -1)
                    return i;

            return -1;
        }

        public static long CalculateChecksum(List<ContentFile> fileblocks)
        {
            long checksum = 0;
            for (var i = 0; i < fileblocks.Count && fileblocks[i].id >= 0; i++)
                checksum += fileblocks[i].id * i;

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