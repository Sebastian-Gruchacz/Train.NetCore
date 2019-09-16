using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace HddTree
{
    class Program
    {
        const bool showFinishedRootsImmediately = true;
        const bool showFiles = true;
            
        const int showTopXSubfolders = 10;
        const int showTopXFiles = 5;

        private const decimal showPercentage = 80m / 100;


        private static string _home;


        static void Main(string[] args)
        {
            string scanRoot = @"C:\";
            _home = Environment.CurrentDirectory;

            var summary = ScanFolder(scanRoot, 0);

            PrintSummary(summary, maxLevel: 5);

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Press ENTER");
            Console.ReadLine();
        }

        private static FolderNode ScanFolder(string path, int level)
        {
            var thisNode = new FolderNode(path, showTopXFiles, showTopXSubfolders);

            string[] subDirectories;
            try
            {
                subDirectories = Directory.GetDirectories(path);
            }
            catch (Exception)
            {
                return null; // skip <junction>
            }

            foreach (var subDirPath in subDirectories)
            {
                var folderNode = (level == 0) 
                    ? LoadFromFolderCache(subDirPath, level) 
                    : ScanFolder(subDirPath, level + 1);

                if (folderNode == null) continue;

                thisNode.AddChild(folderNode);

                if (level == 0)
                {
                    if (showFinishedRootsImmediately)
                    {
                        PrintFolder(folderNode, 0);
                    }

                    SaveFolderCache(folderNode);
                }
            }

            foreach (var file in Directory.GetFiles(path))
            {
                var fNode = new FileNode(file);
                thisNode.AddItem(fNode);
            }

            return thisNode;
        }

        private static FolderNode LoadFromFolderCache(string subDirPath, int level)
        {
            // TODO: implement 
            return ScanFolder(subDirPath, level + 1);
        }

        private static void SaveFolderCache(FolderNode folder)
        {
            // Save cache for later usage
        }

        private static void PrintSummary(FolderNode parentFolder, int maxLevel)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
            Console.WriteLine(@"------------ SUMMARY ------------");
            Console.WriteLine();

            //PrintSummaryLevelTops(parentFolder, 0, maxLevel);
            PrintSummaryLevelPercentages(parentFolder, 0, maxLevel);
        }

        private static void PrintSummaryLevelTops(FolderNode summary, int level, int maxLevel)
        {
            if (level < maxLevel)
            {
                var subFolders = summary.GetTopChildren(showTopXSubfolders);
                foreach (var subFolder in subFolders)
                {
                    PrintFolder(subFolder, level);
                    PrintSummaryLevelTops(subFolder, level + 1, maxLevel);
                }
            }

            if (showFiles)
            {
                foreach (var file in summary.GetTopFiles(showTopXSubfolders))
                {
                    PrintFile(file, level);
                }
            }
        }

        private static void PrintSummaryLevelPercentages(FolderNode parentFolder, int level, int maxLevel)
        {
            if (level < maxLevel)
            {
                var subFolders = parentFolder.GetSortedChildren();
                decimal total = 0.0m;
                foreach (var subFolder in subFolders)
                {
                    if (total > parentFolder.Value * showPercentage)
                    {
                        break;
                    }

                    total += subFolder.Value;

                    PrintFolderPercentage(subFolder, level, parentFolder.Value);
                    PrintSummaryLevelPercentages(subFolder, level + 1, maxLevel);
                }
            }

            if (showFiles)
            {
                decimal total = 0.0m;
                foreach (var file in parentFolder.GetSortedFiles(showTopXSubfolders))
                {
                    if (total > parentFolder.Value * showPercentage)
                    {
                        break;
                    }

                    total += file.Value;

                    PrintFilePercentage(file, level, parentFolder.Value);
                }
            }
        }

        private static void PrintFolderPercentage(FolderNode subFolder, int level, long summaryValue)
        {
            string indent = new string('\t', level);
            decimal folderSize = Math.Round(((decimal)subFolder.Value) / (1024*1024), 2);
            decimal percentage = (summaryValue == 0) ? 0 : ((decimal)subFolder.Value / summaryValue) * 100m;

            Console.ForegroundColor = percentage > 10m ? ConsoleColor.Red : ConsoleColor.White;
            Console.WriteLine($"{indent}[{folderSize:0.00} MB] {subFolder.Name}\t({percentage:0.00}%)");
        }

        private static void PrintFolder(FolderNode folderInfo, int level)
        {
            string indent = new string('\t', level);
            decimal folderSize = Math.Round(((decimal)folderInfo.Value) / (1024*1024), 2);

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine($"{indent}[{folderSize:0.00} MB] {folderInfo.Name}");
        }

        private static void PrintFile(FileNode fileInfo, int level)
        {
            string indent = new string('\t', level + 1);
            decimal folderSize = Math.Round(((decimal)fileInfo.Value) / (1024*1024), 2);

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine($"{indent}[{folderSize:0.00} MB] {fileInfo.Name}");
        }

        private static void PrintFilePercentage(FileNode fileInfo, int level, long summaryValue)
        {
            string indent = new string('\t', level + 1);
            decimal folderSize = Math.Round(((decimal)fileInfo.Value) / (1024*1024), 2);
            decimal percentage = (summaryValue == 0) ? 0 : ((decimal)fileInfo.Value / summaryValue) * 100m;

            Console.ForegroundColor = percentage > 10m ? ConsoleColor.DarkRed : ConsoleColor.Gray;
            Console.WriteLine($"{indent}[{folderSize:0.00} MB] {fileInfo.Name}\t({percentage:0.00}%)");
        }
    }
}
