using System.Collections.Generic;
using Compiler.Error;
using Compiler.Event;
using Compiler.Model;
using Compiler.Input;

namespace Compiler.Parser
{
    public class HeaderParser : ISectorDataParser
    {
        private readonly SectorElementCollection sectorElements;
        private readonly IEventLogger errorLog;

        public HeaderParser(
            SectorElementCollection sectorElements,
            IEventLogger errorLog
        ) {
            this.sectorElements = sectorElements;
            this.errorLog = errorLog;
        }

        public void ParseData(AbstractSectorDataFile data)
        {
            List<HeaderLine> lines = new List<HeaderLine>();
            foreach (SectorData line in data)
            {

                if (line.dataSegments.Count != 0)
                {
                    this.errorLog.AddEvent(
                        new SyntaxError("Cannot define data in file headers", line)
                    );
                    continue;
                }

                lines.Add(
                    new HeaderLine(
                        line.inlineComment,
                        line.definition
                    )    
                );
            }

            this.sectorElements.Add(
                new Header(
                    new Definition(data.FullPath, 0),
                    lines
                )
            );
        }
    }
}
