using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Compiler.Input
{
    abstract public class AbstractSectorDataFile: IEnumerable<string>
    {
        // The current line that has been yielded from the generator
        public int CurrentLine { get; protected set; }

        // The full path to the file
        public string FullPath { get; protected set; }

        public abstract IEnumerator<string> GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
