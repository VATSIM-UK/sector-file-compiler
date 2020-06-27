using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.Parser
{
    public abstract class AbstractEseAirspaceParser : AbstractSectorElementParser
    {
        public AbstractEseAirspaceParser(MetadataParser metadataParser)
            : base(metadataParser)
        {

        }

        public bool IsNewDeclaration(SectorFormatLine line)
        {
            switch (line.dataSegments[0]) {
                case "SECTOR":
                case "SECTORLINE":
                case "CIRCLE_SECTORLINE":
                case "COPX":
                case "FIR_COPX":
                    return true;
                default:
                    return false;
            };
        }
    }
}
