using System.Collections.Generic;
using System.IO;
using System.Linq;
using Compiler.Model;

namespace Compiler.Input
{
    public class HeaderDataFile : AbstractSectorDataFile
    {
        private readonly AbstractSectorDataReader reader;

        public HeaderDataFile(string fullPath, AbstractSectorDataReader reader)
            : base(fullPath, InputDataType.FILE_HEADERS)
        {
            this.reader = reader;
        }

        public override IEnumerator<SectorData> GetEnumerator()
        {
            string line;
            using (StreamReader file = new StreamReader(this.FullPath))
            {
                while ((line = file.ReadLine()) != null)
                {
                    this.CurrentLine = line;
                    this.CurrentLineNumber++;

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
}
