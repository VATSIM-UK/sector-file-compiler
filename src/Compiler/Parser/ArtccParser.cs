using System;
using System.Collections.Generic;
using System.Text;
using Compiler.Model;
using Compiler.Event;
using Compiler.Error;

namespace Compiler.Parser
{
    public class ArtccParser : AbstractSectorElementParser
    {
        private readonly ISectorLineParser sectorLineParser;
        private readonly ArtccType artccType;
        private readonly SectorElementCollection elements;
        private readonly IEventLogger eventLogger;

        public ArtccParser(
            MetadataParser metadataParser,
            ISectorLineParser sectorLineParser,
            ArtccType artccType,
            SectorElementCollection elements,
            IEventLogger eventLogger
        ) : base(metadataParser)
        {
            this.sectorLineParser = sectorLineParser;
            this.artccType = artccType;
            this.elements = elements;
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
                if (sectorData.dataSegments.Count < 5)
                {
                    this.eventLogger.AddEvent(
                        new SyntaxError("Incorrect number of ARTCC segments", data.fileName, i)
                    );
                    continue;
                }

                int count = sectorData.dataSegments.Count;

                // The coordinates are at the end, so work backwards

                // Parse the coordinate, if not parsable, treat it as a fix.
                Coordinate endCoordinate = CoordinateParser.Parse(sectorData.dataSegments[count - 2], sectorData.dataSegments[count - 1]);
                if (endCoordinate.Equals(CoordinateParser.invalidCoordinate))
                {
                    this.eventLogger.AddEvent(
                        new SyntaxError("Invalid ARTCC end coordinate format: " + data.lines[i], data.fileName, i)
                    );
                    return;
                }

                Coordinate startCoordinate = CoordinateParser.Parse(sectorData.dataSegments[count - 4], sectorData.dataSegments[count - 3]);
                if (startCoordinate.Equals(CoordinateParser.invalidCoordinate))
                {
                    this.eventLogger.AddEvent(
                        new SyntaxError("Invalid ARTCC start coordinate format: " + data.lines[i], data.fileName, i)
                    );
                    return;
                }

                // Add it
                this.elements.Add(
                    new Artcc(
                        string.Join(" ", sectorData.dataSegments.GetRange(0, count - 4)),
                        this.artccType,
                        startCoordinate,
                        endCoordinate,
                        sectorData.comment
                    )
                );
            }
        }
    }
}
