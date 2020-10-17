using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.Output
{
    public class OutputGroup
    {
        public SortedSet<string> FileList { get; }
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

        public bool Equals(OutputGroup compare)
        {
            return compare.Key == this.Key;
        }
    }
}
