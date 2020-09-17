using System;
using System.Collections.Generic;
using System.Text;
using Compiler.Model;
using Compiler.Event;
using Compiler.Error;
using Compiler.Input;

namespace Compiler.Parser
{
    public class FixParser : AbstractSectorElementParser, ISectorDataParser
    {
        private readonly SectorElementCollection elements;
        private readonly ISectorLineParser sectorLineParser;
        private readonly IEventLogger eventLogger;

        public FixParser(
            MetadataParser metadataParser,
            ISectorLineParser sectorLineParser,
            SectorElementCollection elements,
            IEventLogger eventLogger
        ) : base(metadataParser)
        {
            this.elements = elements;
            this.sectorLineParser = sectorLineParser;
            this.eventLogger = eventLogger;
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

                if (sectorData.dataSegments.Count != 3)
                {
                    this.eventLogger.AddEvent(
                        new SyntaxError("Incorrect number of FIX segments", data.FullPath, data.CurrentLineNumber)
                    );
                    continue;
                }

                // Parse the coordinate
                Coordinate parsedCoordinate = CoordinateParser.Parse(sectorData.dataSegments[1], sectorData.dataSegments[2]);
                if (parsedCoordinate.Equals(CoordinateParser.invalidCoordinate))
                {
                    this.eventLogger.AddEvent(
                        new SyntaxError("Invalid coordinate format: " + data.CurrentLine, data.FullPath, data.CurrentLineNumber)
                    );
                    return;
                }

                this.elements.Add(new Fix(sectorData.dataSegments[0], parsedCoordinate, sectorData.comment));
            }
        }
    }
}
