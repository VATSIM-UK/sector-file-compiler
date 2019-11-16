using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.Parser
{
    public interface ISectorLineParser
    {
        public SectorFormatLine ParseLine(string line);
    }
}
