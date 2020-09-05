using System;
using System.Collections.Generic;
using Compiler.Model;
using Compiler.Event;
using Compiler.Error;
using Compiler.Input;
using System.Linq;

namespace Compiler.Parser
{
    public class VorParser : AbstractSectorElementParser, ISectorDataParser
    {
        private readonly SectorElementCollection elements;
        private readonly ISectorLineParser sectorLineParser;
        private readonly IFrequencyParser frequencyParser;
        private readonly IEventLogger eventLogger;

        public VorParser(
            MetadataParser metadataParser,
            ISectorLineParser sectorLineParser,
            IFrequencyParser frequencyParser,
            SectorElementCollection elements,
            IEventLogger eventLogger
        ) : base(metadataParser)
        {
            this.elements = elements;
            this.sectorLineParser = sectorLineParser;
            this.frequencyParser = frequencyParser;
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
                if (sectorData.dataSegments.Count != 4)
                {
                    this.eventLogger.AddEvent(
                        new SyntaxError("Incorrect number of VOR segments", data.FullPath, data.CurrentLineNumber)
                    );
                    continue;
                }

                // Check the identifier
                if (
                    sectorData.dataSegments[0].Any(char.IsDigit) ||
                    (sectorData.dataSegments[0].Length != 2 && sectorData.dataSegments[0].Length != 3)
                ) {
                    this.eventLogger.AddEvent(
                        new SyntaxError("Invalid VOR identifier: " + sectorData.dataSegments[1], data.FullPath, data.CurrentLineNumber)
                    );
                    return;
                }

                // Parse the frequency
                if (this.frequencyParser.ParseFrequency(sectorData.dataSegments[1]) == null)
                {
                    this.eventLogger.AddEvent(
                        new SyntaxError("Invalid VOR frequency: " + sectorData.dataSegments[1], data.FullPath, data.CurrentLineNumber)
                    );
                    return;
                }

                // Parse the coordinate
                Coordinate parsedCoordinate = CoordinateParser.Parse(sectorData.dataSegments[2], sectorData.dataSegments[3]);
                if (parsedCoordinate.Equals(CoordinateParser.invalidCoordinate))
                {
                    this.eventLogger.AddEvent(
                        new SyntaxError("Invalid coordinate format: " + line, data.FullPath, data.CurrentLineNumber)
                    );
                    return;
                }

                this.elements.Add(
                    new Vor(sectorData.dataSegments[0], sectorData.dataSegments[1], parsedCoordinate, sectorData.comment)
                );
            }
        }
    }
}
