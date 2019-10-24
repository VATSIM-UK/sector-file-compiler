using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.Model
{
    interface ISectorElementParser
    {
        public void ParseElements(string filename, List<string> lines, SectorElementCollection sectorElements);
    }
}
