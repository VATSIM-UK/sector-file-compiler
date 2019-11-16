using System;
using System.Collections.Generic;

namespace Compiler.Parser
{
    public struct SectorFormatLine
    {
        // The raw data section of the line
        public readonly string data;

        /* 
         * The data segmented by the rules of whatever part of the sector is being processed 
         * (usually just spaces between each item).
         */
        public readonly List<string> dataSegments;

        // Any comment attached to the end of the data line
        public readonly string comment;

        public SectorFormatLine(string data, List<string> dataSegments, string comment)
        {
            this.data = data;
            this.dataSegments = dataSegments;
            this.comment = comment;
        }

        public bool Equals(SectorFormatLine compare)
        {
            return this.Equals(this, compare);
        }

        public bool Equals(SectorFormatLine a, SectorFormatLine b)
        {
            if (
                a.data != b.data ||
                a.comment != b.comment ||
                a.dataSegments.Count != b.dataSegments.Count
            )
            {
                return false;
            }

            for (int i = 0; i < this.dataSegments.Count; i++)
            {
                if (a.dataSegments[i] != b.dataSegments[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
