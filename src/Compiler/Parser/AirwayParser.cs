using System;
using System.Collections.Generic;
using System.Text;
using Compiler.Model;
using Compiler.Event;
using Compiler.Error;

namespace Compiler.Parser
{
    public class AirwayParser : AbstractSectorElementParser
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
                if (sectorData.dataSegments.Count != 4)
                {
                    this.eventLogger.AddEvent(
                        new SyntaxError("Incorrect number of Airway segments", data.fullPath, i)
                    );
                    continue;
                }

                // Parse the airway segment point
                Point startPoint = PointParser.Parse(sectorData.dataSegments[0], sectorData.dataSegments[1]);
                if (startPoint.Equals(PointParser.invalidPoint))
                {
                    this.eventLogger.AddEvent(
                        new SyntaxError("Invalid Airway start point format: " + data.lines[i], data.fullPath, i)
                    );
                    return;
                }


                // Parse the segment endpoint
                Point endPoint = PointParser.Parse(sectorData.dataSegments[2], sectorData.dataSegments[3]);
                if (endPoint.Equals(PointParser.invalidPoint))
                {
                    this.eventLogger.AddEvent(
                        new SyntaxError("Invalid Airway end point format: " + data.lines[i], data.fullPath, i)
                    );
                    return;
                }

                // Add it
                this.elements.Add(
                    new Airway(
                        data.fileName,
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
