using System;
using System.Collections.Generic;
using System.IO;

namespace HddTree
{
    internal class FolderNode : IThresholdItem
    {
        private readonly ThresholdCollection<FileNode> _fileCollection;
        private readonly ThresholdCollection<FolderNode> _folderCollection;

        public string Name { get; private set; }

        public DateTime LastModified { get; private set; }

        public FolderNode(string path, int fileCountThreshold, int directoryCountThreshold)
        {
            var info = new DirectoryInfo(path);
            this.Name = info.FullName;
            this.LastModified = info.LastWriteTimeUtc;

            // register files larger than 1 MB only (butt add those small to total anyway...)
            this._fileCollection = new ThresholdCollection<FileNode>(fileCountThreshold * 2, 1024*1024);

            // also, ignore small subfolders
            this._folderCollection = new ThresholdCollection<FolderNode>(directoryCountThreshold * 2, 1024*1024);
        }

        public long Value => this._fileCollection.TotalValue + this._folderCollection.TotalValue;

        public void AddChild(FolderNode child)
        {
            this._folderCollection.Add(child);
        }

        public void AddItem(FileNode fNode)
        {
            this._fileCollection.Add(fNode);
        }

        public IEnumerable<FileNode> GetTopFiles(int count)
        {
            return this._fileCollection.GetTop(count);
        }

        public IEnumerable<FolderNode> GetTopChildren(int count)
        {
            return this._folderCollection.GetTop(count);
        }

        public IEnumerable<FolderNode> GetSortedChildren()
        {
            return this._folderCollection.GetAllSorted();
        }

        public IEnumerable<FileNode> GetSortedFiles(int showTopXSubfolders)
        {
            return this._fileCollection.GetAllSorted();
        }
    }
}