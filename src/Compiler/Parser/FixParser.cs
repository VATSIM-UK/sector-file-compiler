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
        private readonly IEventLogger eventLogger;

        public FixParser(MetadataParser metadataParser, SectorElementCollection elements, IEventLogger eventLogger) : base(metadataParser)
        {
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

                SectorFormatLine sectorData = this.ParseLine(data.lines[i]);

                string[] parts = sectorData.data.Split(' ');
                if (parts.Length != 3)
                {
                    this.eventLogger.AddEvent(
                        new SyntaxError("Incorrect number of FIX segments", data.fileName, i)
                    );
                    continue;
                }

                // Parse the coordinate
                Coordinate parsedCoordinate = CoordinateParser.Parse(parts[1], parts[2]);
                if (parsedCoordinate.Equals(CoordinateParser.invalidCoordinate))
                {
                    this.eventLogger.AddEvent(
                        new SyntaxError("Invalid coordinate format: " + data.lines[i], data.fileName, i)
                    );
                    return;
                }

                this.elements.Add(new Fix(parts[0], parsedCoordinate, sectorData.comment));
            }
        }
    }
}
