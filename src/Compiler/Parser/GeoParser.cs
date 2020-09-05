using System;
using System.Collections.Generic;
using Compiler.Model;
using Compiler.Event;
using Compiler.Error;
using Compiler.Input;
using System.Linq;

namespace Compiler.Parser
{
    public class GeoParser : AbstractSectorElementParser, ISectorDataParser
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

        public void ParseData(AbstractSectorDataFile data)
        {
            bool foundFirst = false;
            string name = "";
            List<GeoSegment> segments = new List<GeoSegment>();
            foreach (string line in data)
            {
                // Defer all metadata lines to the base
                if (this.ParseMetadata(line))
                {
                    continue;
                }

                SectorFormatLine sectorData = this.sectorLineParser.ParseLine(line);
                // Find the name
                if (!foundFirst)
                {
                    name = sectorData.dataSegments[0];
                    sectorData.dataSegments.RemoveAt(0);
                    foundFirst = true;
                }

                /*
                 * In some places in the UKSF, we define this random point to make sure drawing works properly.
                 * If we see it, just insert it.
                 */
                if (sectorData.data.Contains(GeoParser.noDataString))
                {
                    segments.Add(
                        new GeoSegment(
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
                        new SyntaxError("Incorrect number parts for GEO segment", data.FullPath, data.CurrentLineNumber)
                    );
                    return;
                }

                // Parse the first coordinate
                Point parsedStartPoint = PointParser.Parse(sectorData.dataSegments[0], sectorData.dataSegments[1]);
                if (parsedStartPoint.Equals(PointParser.invalidPoint))
                {
                    this.eventLogger.AddEvent(
                        new SyntaxError("Invalid GEO segment point format: " + data.CurrentLine, data.FullPath, data.CurrentLineNumber)
                    );
                    return;
                }

                // Parse the end coordinate
                Point parsedEndPoint = PointParser.Parse(sectorData.dataSegments[2], sectorData.dataSegments[3]);
                if (parsedEndPoint.Equals(PointParser.invalidPoint))
                {
                    this.eventLogger.AddEvent(
                        new SyntaxError("Invalid GEO segment point format: " + data.CurrentLine, data.FullPath, data.CurrentLineNumber)
                    );
                    return;
                }

                // Add segment
                segments.Add(
                    new GeoSegment(parsedStartPoint, parsedEndPoint, sectorData.dataSegments[4], sectorData.comment)
                );
            }

            // Check segment count
            if (segments.Count == 0)
            {
                this.eventLogger.AddEvent(
                    new SyntaxError("Expected GEO segements in file", data.FullPath, 0)
                );
                return;
            }

            // Add final geo element
            this.elements.Add(new Geo(name, segments));
        }
    }
}
