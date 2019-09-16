using System;
using System.IO;

namespace HddTree
{
    internal class FileNode : IThresholdItem
    {
        public string Name { get; private set; }

        public DateTime LastModified { get; private set; }

        public long Value { get; private set; }

        public FileNode(string filePath)
        {
            var info = new FileInfo(filePath);

            this.Name = info.FullName;
            this.LastModified = info.LastWriteTimeUtc;
            this.Value = info.Length;
        }
    }
}