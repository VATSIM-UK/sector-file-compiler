using System;
using System.Collections.Generic;
using Compiler.Model;
using Compiler.Event;
using Compiler.Error;
using System.Linq;

namespace Compiler.Parser
{
    public class GeoParser : AbstractSectorElementParser
    {
        private readonly SectorElementCollection elements;
        private readonly ISectorLineParser sectorLineParser;
        private readonly IEventLogger eventLogger;

        private static readonly string noDataString = "S999.00.00.000 E999.00.00.000";

        public GeoParser(
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

                /*
                 * In some places in the UKSF, we define this random point to make sure drawing works properly.
                 * If we see it, just insert it.
                 */
                if (sectorData.data.Contains(GeoParser.noDataString))
                {
                    this.elements.Add(
                        new Geo(
                            new Point(new Coordinate("S999.00.00.000", "E999.00.00.000")),
                            new Point(new Coordinate("S999.00.00.000", "E999.00.00.000")),
                            "0",
                            "Compiler inserted line"
                        )
                    );
                    continue;
                }


                if (sectorData.dataSegments.Count != 5)
                {
                    this.eventLogger.AddEvent(
                        new SyntaxError("Incorrect number of GEO segments", data.fullPath, i + 1)
                    );
                    continue;
                }

                // Parse the first coordinate
                Point parsedStartPoint = PointParser.Parse(sectorData.dataSegments[0], sectorData.dataSegments[1]);
                if (parsedStartPoint.Equals(PointParser.invalidPoint))
                {
                    this.eventLogger.AddEvent(
                        new SyntaxError("Invalid GEO point format: " + data.lines[i], data.fullPath, i + 1)
                    );
                    continue;
                }

                // Parse the end coordinate
                Point parsedEndPoint = PointParser.Parse(sectorData.dataSegments[2], sectorData.dataSegments[3]);
                if (parsedEndPoint.Equals(PointParser.invalidPoint))
                {
                    this.eventLogger.AddEvent(
                        new SyntaxError("Invalid GEO point format: " + data.lines[i], data.fullPath, i + 1)
                    );
                    continue;
                }

                // Add element
                this.elements.Add(
                    new Geo(parsedStartPoint, parsedEndPoint, sectorData.dataSegments[4], sectorData.comment)
                );
            }
        }
    }
}
