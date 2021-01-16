using System;
using System.Collections.Generic;

namespace Compiler.Output
{
    public class OutputGroup: IComparable<OutputGroup>
    {
        public SortedSet<string> FileList { get; } = new SortedSet<string>();
        public string Key { get; }
        public string HeaderDescription { get; }

        public OutputGroup(string key)
        {
            this.Key = key;
            this.HeaderDescription = null;
        }

        public OutputGroup(string key, string headerDescription)
        {
            this.Key = key;
            HeaderDescription = headerDescription;
        }

        public void AddFile(string path)
        {
            this.FileList.Add(path);
        }

        public void Merge(OutputGroup group)
        {
            this.FileList.UnionWith(group.FileList);
        }

        public bool Equals(OutputGroup compare)
        {
            return compare.Key == this.Key;
        }

        public int CompareTo(OutputGroup obj)
        {
            return this.Key.CompareTo(obj.Key);
        }

        public int CountFiles()
        {
            return this.FileList.Count;
        }
    }
}
