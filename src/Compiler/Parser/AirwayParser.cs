using System;
using System.Collections.Generic;
using System.Text;
using Compiler.Model;
using Compiler.Event;
using Compiler.Error;
using Compiler.Input;

namespace Compiler.Parser
{
    public class AirwayParser : AbstractSectorElementParser, ISectorDataParser
    {
        private readonly ISectorLineParser sectorLineParser;
        private readonly AirwayType airwayType;
        private readonly SectorElementCollection elements;
        private readonly IEventLogger eventLogger;

        public AirwayParser(
            MetadataParser metadataParser,
            ISectorLineParser sectorLineParser,
            AirwayType airwayType,
            SectorElementCollection elements,
            IEventLogger eventLogger
        ) : base(metadataParser)
        {
            this.sectorLineParser = sectorLineParser;
            this.airwayType = airwayType;
            this.elements = elements;
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
                        new SyntaxError("Incorrect number of Airway segments", data.FullPath, data.CurrentLineNumber)
                    );
                    continue;
                }

                // Parse the airway segment point
                Point startPoint = PointParser.Parse(sectorData.dataSegments[0], sectorData.dataSegments[1]);
                if (startPoint.Equals(PointParser.invalidPoint))
                {
                    this.eventLogger.AddEvent(
                        new SyntaxError("Invalid Airway start point format: " + data.CurrentLine, data.FullPath, data.CurrentLineNumber)
                    );
                    return;
                }


                // Parse the segment endpoint
                Point endPoint = PointParser.Parse(sectorData.dataSegments[2], sectorData.dataSegments[3]);
                if (endPoint.Equals(PointParser.invalidPoint))
                {
                    this.eventLogger.AddEvent(
                        new SyntaxError("Invalid Airway end point format: " + data.CurrentLine, data.FullPath, data.CurrentLineNumber)
                    );
                    return;
                }

                // Add it
                this.elements.Add(
                    new Airway(
                        "Test",
                        this.airwayType,
                        startPoint,
                        endPoint,
                        sectorData.comment
                    )
                );
            }
        }
    }
}
