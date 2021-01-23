using System.Collections.Generic;
using System.IO;
using System.Linq;
using Compiler.Model;

namespace Compiler.Input
{
    public class HeaderDataFile : AbstractSectorDataFile
    {
        private readonly IInputStreamFactory streamFactory;
        private readonly AbstractSectorDataReader reader;

        public HeaderDataFile(string fullPath, IInputStreamFactory streamFactory, AbstractSectorDataReader reader)
            : base(fullPath, InputDataType.FILE_HEADERS)
        {
            this.streamFactory = streamFactory;
            this.reader = reader;
        }

        public override IEnumerator<SectorData> GetEnumerator()
        {
            string line;
            using TextReader file = this.streamFactory.GetStream(this.FullPath);
            while ((line = file.ReadLine()) != null)
            {
                CurrentLine = line;
                CurrentLineNumber++;

                // Ignore blank lines
                if (line.Trim().Length == 0)
                {
                    continue;
                }

                yield return new SectorData(
                    new Docblock(),
                    this.reader.GetCommentSegment(line),
                    this.reader.GetDataSegments(line).Where(s => !string.IsNullOrEmpty(s)).ToList(),
                    this.reader.GetRawData(line),
                    new Definition(this.FullPath, this.CurrentLineNumber)
                );
            }
        }
    }
}
