using System.Collections.Generic;
using Compiler.Model;
using Compiler.Error;
using Compiler.Event;
using Compiler.Input;

namespace Compiler.Parser
{
    public class SidStarParser: ISectorDataParser
    {
        private readonly SectorElementCollection sectorElements;
        private readonly IEventLogger errorLog;

        public SidStarParser(
            SectorElementCollection sectorElements,
            IEventLogger errorLog
        ) {
            this.sectorElements = sectorElements;
            this.errorLog = errorLog;
        }

        public void ParseData(AbstractSectorDataFile data)
        {
            foreach (SectorData line in data)
            {
                if (line.dataSegments.Count != 5)
                {
                    this.errorLog.AddEvent(
                        new SyntaxError("Incorrect number of SIDSTAR segments", line)
                    );
                    continue;
                }

                if (line.dataSegments[0] != "SID" && line.dataSegments[0] != "STAR")
                {
                    this.errorLog.AddEvent(
                        new SyntaxError("Unknown SIDSTAR type " + line.dataSegments[0], line)
                    ); ;
                    continue;
                }

                this.sectorElements.Add(
                    new SidStar(
                        line.dataSegments[0],
                        line.dataSegments[1],
                        line.dataSegments[2],
                        line.dataSegments[3],
                        new List<string>(line.dataSegments[4].Split(' ')),
                        line.definition,
                        line.docblock,
                        line.inlineComment
                    )
                );
            }
        }
    }
}
