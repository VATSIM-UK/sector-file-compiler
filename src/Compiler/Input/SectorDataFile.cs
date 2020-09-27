using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Compiler.Model;

namespace Compiler.Input
{
    public class SectorDataFile: AbstractSectorDataFile
    {
        private readonly AbstractSectorDataReader reader;
        public SectorDataFile(string fullPath, AbstractSectorDataReader reader)
        {
            this.FullPath = fullPath;
            this.reader = reader;
        }

        public override IEnumerator<SectorData> GetEnumerator()
        {
            string line;
            Docblock docblock = new Docblock();
            using (StreamReader file = new StreamReader(this.FullPath))
            {
                while ((line = file.ReadLine()) != null)
                {
                    this.CurrentLineNumber++;
                    if (reader.IsBlankLine(line))
                    {
                        continue;
                    }
                    else if (reader.IsCommentLine(line))
                    {
                        docblock.AddLine(reader.GetCommentSegment(line));
                    }
                    else
                    {
                        yield return new SectorData(
                            docblock,
                            reader.GetCommentSegment(line),
                            reader.GetDataSegments(line),
                            new Definition(this.FullPath, this.CurrentLineNumber)
                        );
                        docblock = new Docblock();
                    }
                }
            }
        }
    }
}
