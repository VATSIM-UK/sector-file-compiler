using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Compiler.Input
{
    abstract public class AbstractSectorDataFile: IEnumerable<SectorData>
    {
        // The number of the current line
        public int CurrentLineNumber { get; protected set; } = 0;

        // The last line yielded by the generator
        public string CurrentLine { get; protected set; } = "";

        // The full path to the file
        public string FullPath { get; protected set; }

        // The type of data this file contains
        public InputDataType DataType { get; }

        public AbstractSectorDataFile(InputDataType dataType)
        {
            DataType = dataType;
        }

        public bool Equals(AbstractSectorDataFile compare)
        {
            return this.FullPath == compare.FullPath;
        }

        public string GetParentDirectory()
        {
            return Path.GetDirectoryName(this.FullPath);
        }

        public abstract IEnumerator<SectorData> GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
