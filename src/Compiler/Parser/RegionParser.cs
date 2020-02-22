using System;
using System.Collections.Generic;
using Compiler.Model;
using Compiler.Event;
using Compiler.Error;
using System.Linq;

namespace Compiler.Parser
{
    public class RegionParser : AbstractSectorElementParser
    {
        private readonly SectorElementCollection elements;
        private readonly ISectorLineParser sectorLineParser;
        private readonly IEventLogger eventLogger;

        public RegionParser(
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
            string colour = "";
            List<Point> points = new List<Point>();
            string firstLineComment = "";
            bool foundFirst = false;
            for (int i = 0; i < data.lines.Count; i++)
            {
                // Defer all metadata lines to the base
                if (this.ParseMetadata(data.lines[i]))
                {
                    continue;
                }

                SectorFormatLine sectorData = this.sectorLineParser.ParseLine(data.lines[i]);

                if (i == 0 && sectorData.dataSegments.Count != 3)
                {
                    this.eventLogger.AddEvent(
                        new SyntaxError("Invalid first line of region " + data.lines[i], data.fullPath, i + 1)
                    );
                    return;
                }

                /**
                 * We've found a segment with 3 parts, so must be a new declaration.
                 */
                if (sectorData.dataSegments.Count == 3)
                {
                    // If it's not the first in the data stream, then it's a new declaration, save the previous
                    if (foundFirst)
                    {
                        this.elements.Regions.Add(
                            new Region(
                                colour,
                                points,
                                firstLineComment
                            )
                        );

                        // Clear the points array
                        points = new List<Point>();
                    } else {
                        foundFirst = true;
                    }

                    // Setup a new segment
                    Point parsedNewPoint = PointParser.Parse(sectorData.dataSegments[1], sectorData.dataSegments[2]);
                    if (parsedNewPoint.Equals(PointParser.invalidPoint))
                    {
                        this.eventLogger.AddEvent(
                            new SyntaxError("Invalid region point format: " + data.lines[i], data.fullPath, i + 1)
                        );
                        return;
                    }

                    colour = sectorData.dataSegments[0];
                    firstLineComment = sectorData.comment;
                    points.Add(parsedNewPoint);
                    continue;
                }

                if (!sectorData.data.StartsWith(" "))
                {
                    this.eventLogger.AddEvent(
                        new SyntaxError("All continuous regions must start with a space " + data.lines[i], data.fullPath, i + 1)
                    );
                    return;
                }

                // Parse the coordinate
                Point parsedPoint = PointParser.Parse(sectorData.dataSegments[0], sectorData.dataSegments[1]);
                if (parsedPoint == PointParser.invalidPoint)
                {
                    this.eventLogger.AddEvent(
                        new SyntaxError("Invalid region point format: " + data.lines[i], data.fullPath, i + 1)
                    );
                    return;
                }

                points.Add(parsedPoint);
            }

            // Add the last element
            this.elements.Regions.Add(
                new Region(
                    colour,
                    points,
                    firstLineComment
                )
            );
        }

        private bool ValidPoint(string lat, string lon)
        {
            return PointParser.Parse(lat, lon) != PointParser.invalidPoint;
        }
    }
}
