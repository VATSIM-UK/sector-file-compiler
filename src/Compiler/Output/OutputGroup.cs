using System;

namespace Compiler.Output
{
    public class OutputGroup: IComparable<OutputGroup>
    {
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

        public bool Equals(OutputGroup compare)
        {
            return compare.Key == this.Key;
        }

        public int CompareTo(OutputGroup obj)
        {
            return string.Compare(this.Key, obj.Key, StringComparison.Ordinal);
        }
    }
}
