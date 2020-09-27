using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Compiler.Input
{
    abstract public class AbstractSectorDataFile: IEnumerable<SectorData>, IComparable
    {
        // The number of the current line
        public int CurrentLineNumber { get; protected set; } = 0;

        // The last line yielded by the generator
        public string CurrentLine { get; protected set; } = "";

        // The full path to the file
        public string FullPath { get; protected set; }

        public int CompareTo(object obj)
        {
            return this.FullPath.CompareTo(((AbstractSectorDataFile)obj).FullPath);
        }

        public abstract IEnumerator<SectorData> GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
