using System.Collections.Generic;
using System.IO;
using Compiler.Model;

namespace Compiler.Input
{
    public class SectorDataFile: AbstractSectorDataFile
    {
        private readonly IInputStreamFactory streamFactory;
        private readonly AbstractSectorDataReader reader;

        public SectorDataFile(string fullPath, IInputStreamFactory streamFactory, InputDataType dataType, AbstractSectorDataReader reader)
            : base(fullPath, dataType)
        {
            this.streamFactory = streamFactory;
            this.reader = reader;
        }

        /*
         * Iterate the lines in a file.
         * - Skip any blank line
         * - Store up any full-comment lines to be turned into a DocBlock
         * - If it's a data line, yield a data item for parsing
         */
        public override IEnumerator<SectorData> GetEnumerator()
        {
            Docblock docblock = new Docblock();
            using TextReader file = this.streamFactory.GetStream(this.FullPath);
            string line;
            while ((line = file.ReadLine()) != null)
            {
                this.CurrentLineNumber++;
                if (reader.IsBlankLine(line))
                {
                    continue;
                }

                if (reader.IsCommentLine(line))
                {
                    docblock.AddLine(reader.GetCommentSegment(line));
                }
                else
                {
                    yield return new SectorData(
                        docblock,
                        reader.GetCommentSegment(line),
                        reader.GetDataSegments(line),
                        reader.GetRawData(line),
                        new Definition(this.FullPath, this.CurrentLineNumber)
                    );
                    docblock = new Docblock();
                }
            }
        }
    }
}
