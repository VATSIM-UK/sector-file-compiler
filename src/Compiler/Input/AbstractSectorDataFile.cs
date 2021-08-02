using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace Compiler.Input
{
    abstract public class AbstractSectorDataFile: IEnumerable<SectorData>
    {
        // The number of the current line
        public int CurrentLineNumber { get; protected set; } = 0;

        // The last line yielded by the generator
        public string CurrentLine { get; protected set; } = "";

        // The full path to the file
        public string FullPath { get; }

        // The type of data this file contains
        public InputDataType DataType { get; }

        public AbstractSectorDataFile(string fullPath, InputDataType dataType)
        {
            FullPath = fullPath;
            DataType = dataType;
        }

        public override bool Equals(object obj)
        {
            return obj is AbstractSectorDataFile file && Equals(file);
        }

        public override int GetHashCode()
        {
            return (FullPath != null ? FullPath.GetHashCode() : 0);
        }

        public bool Equals(AbstractSectorDataFile compare)
        {
            return FullPath == compare.FullPath;
        }

        public string GetParentDirectoryName()
        {
            return Path.GetFileName(Path.GetDirectoryName(FullPath));
        }

        public string GetFileName()
        {
            return Path.GetFileNameWithoutExtension(FullPath);
        }

        public abstract IEnumerator<SectorData> GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
