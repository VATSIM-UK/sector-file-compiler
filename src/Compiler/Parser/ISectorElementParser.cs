using System.Collections.Generic;
using Compiler.Model;

namespace Compiler.Parser
{
    public interface ISectorElementParser
    {
        public void ParseElements(string filename, List<string> lines, SectorElementCollection sectorElements);
    }
}
