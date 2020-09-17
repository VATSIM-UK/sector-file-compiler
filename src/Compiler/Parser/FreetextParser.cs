using System.Collections.Generic;
using Compiler.Model;
using Compiler.Error;
using Compiler.Event;
using Compiler.Input;

namespace Compiler.Parser
{
    public class FreetextParser : AbstractSectorElementParser, ISectorDataParser
    {
        private readonly ISectorLineParser sectorLineParser;
        private readonly SectorElementCollection sectorElements;
        private readonly IEventLogger errorLog;

        public FreetextParser(
            MetadataParser metadataParser,
            ISectorLineParser sectorLineParser,
            SectorElementCollection sectorElements,
            IEventLogger errorLog
        ) : base(metadataParser)
        {
            this.sectorLineParser = sectorLineParser;
            this.sectorElements = sectorElements;
            this.errorLog = errorLog;
        }

        public void ParseData(AbstractSectorDataFile data)
        {
            foreach (string line in data)
            {
                // Defer all metadata lines to the base
                if (this.ParseMetadata(line))
                {
                    continue;
                }

                SectorFormatLine sectorData = this.sectorLineParser.ParseLine(line);
                if (sectorData.dataSegments.Count != 4)
                {
                    this.errorLog.AddEvent(
                        new SyntaxError("Incorrect number of Freetext segments", data.FullPath, data.CurrentLineNumber)
                    );
                    return;
                }

                Coordinate parsedCoordinate = CoordinateParser.Parse(sectorData.dataSegments[0], sectorData.dataSegments[1]);
                if (parsedCoordinate.Equals(CoordinateParser.invalidCoordinate))
                {
                    this.errorLog.AddEvent(
                        new SyntaxError("Invalid coordinate format: " + data.CurrentLine, data.FullPath, data.CurrentLineNumber)
                    );
                    return;
                }


                this.sectorElements.Add(
                    new Freetext(
                        sectorData.dataSegments[2],
                        sectorData.dataSegments[3],
                        parsedCoordinate,
                        sectorData.comment
                    )
                );
            }
        }
    }
}
