using System.Collections.Generic;

namespace Compiler.Parser
{
    /**
     * A struct to hold data in the sectorfile format.
     */
    public struct SectorFormatData
    {
        public SectorFormatData(string fullPath, string fileName, string parentDirectory, List<string> lines)
        {
            this.fullPath = fullPath;
            this.fileName = fileName;
            this.parentDirectory = parentDirectory;
            this.lines = lines;
        }

        public readonly string fullPath;

        public readonly string fileName;

        public readonly string parentDirectory;

        public readonly List<string> lines;
    }
}
