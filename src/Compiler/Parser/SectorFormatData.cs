using System.Collections.Generic;

namespace Compiler.Parser
{
    /**
     * A struct to hold data in the sectorfile format.
     */
    public struct SectorFormatData
    {
        public SectorFormatData(string fileName, List<string> lines)
        {
            this.fileName = fileName;
            this.lines = lines;
        }

        public readonly string fileName;

        public readonly List<string> lines;
    }
}
