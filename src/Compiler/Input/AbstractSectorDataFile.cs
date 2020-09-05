using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Compiler.Input
{
    abstract public class AbstractSectorDataFile: IEnumerable<string>
    {
        // The number of the current line
        public int CurrentLineNumber { get; protected set; } = 0;

        // The last line yielded by the generator
        public string CurrentLine { get; protected set; } = "";

        // The full path to the file
        public string FullPath { get; protected set; }

        public abstract IEnumerator<string> GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
