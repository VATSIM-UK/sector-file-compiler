using System;
using System.Collections.Generic;
using System.Text;
using Compiler.Model;

namespace Compiler.Input
{
    /*
     * Represents a single item (line) of sector data, that may or may not consitute
     * a full model.
     */
    public struct SectorData
    {
        public SectorData(Docblock docblock, Comment inlineComment, List<string> dataSegments)
        {
            this.docblock = docblock;
            this.inlineComment = inlineComment;
            this.dataSegments = dataSegments;
        }

        // The docblock above this line of sector data
        public readonly Docblock docblock;

        // The inline comment for this line of sector data
        public readonly Comment inlineComment;

        // The segmented data based on whatever delimiter is used
        public readonly List<string> dataSegments;
    }
}
