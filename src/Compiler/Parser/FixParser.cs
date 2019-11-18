using System;
using System.Collections.Generic;
using System.Text;
using Compiler.Model;
using Compiler.Event;
using Compiler.Error;

namespace Compiler.Parser
{
    public class FixParser : AbstractSectorElementParser
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

        public override void ParseData(SectorFormatData data)
        {
            for (int i = 0; i < data.lines.Count; i++)
            {
                // Defer all metadata lines to the base
                if (this.ParseMetadata(data.lines[i]))
                {
                    continue;
                }

                SectorFormatLine sectorData = this.sectorLineParser.ParseLine(data.lines[i]);

                if (sectorData.dataSegments.Count != 3)
                {
                    this.eventLogger.AddEvent(
                        new SyntaxError("Incorrect number of FIX segments", data.fullPath, i)
                    );
                    continue;
                }

                // Parse the coordinate
                Coordinate parsedCoordinate = CoordinateParser.Parse(sectorData.dataSegments[1], sectorData.dataSegments[2]);
                if (parsedCoordinate.Equals(CoordinateParser.invalidCoordinate))
                {
                    this.eventLogger.AddEvent(
                        new SyntaxError("Invalid coordinate format: " + data.lines[i], data.fullPath, i)
                    );
                    return;
                }

                this.elements.Add(new Fix(sectorData.dataSegments[0], parsedCoordinate, sectorData.comment));
            }
        }
    }
}
