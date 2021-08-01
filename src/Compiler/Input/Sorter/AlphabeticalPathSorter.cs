using System;

namespace Compiler.Input.Sorter
{
    public class AlphabeticalPathSorter: IFileSorter
    {
        public int Compare(string x, string y)
        {
            return string.Compare(x, y, StringComparison.InvariantCulture);
        }
    }
}
