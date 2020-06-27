using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.Parser
{
    /*
     * An interface for the classes that parse files
     * to extract data.
     */
    public interface IFileParser
    {
        public void ParseData(SectorFormatData data);
    }
}
