using System;
using System.Collections.Generic;
using Compiler.Model;
using Compiler.Event;
using Compiler.Error;
using System.Linq;

namespace Compiler.Parser
{
    public class RegionParser : AbstractSectorElementParser, IFileParser
    {
        private readonly SectorElementCollection elements;
        private readonly ISectorLineParser sectorLineParser;
        private readonly IEventLogger eventLogger;
        const string regionNameDeclaration = "REGIONNAME";

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

        public void ParseData(SectorFormatData data)
        {
            string colour = "";
            List<Point> points = new List<Point>();
            string firstLineComment = "";
            bool foundFirst = false;
            string regionName = "";
            bool expectingColourDefinition = false;
            for (int i = 0; i < data.lines.Count; i++)
            {
                // Defer all metadata lines to the base
                if (this.ParseMetadata(data.lines[i]))
                {
                    continue;
                }

                SectorFormatLine sectorData = this.sectorLineParser.ParseLine(data.lines[i]);

                // Check for the first REGIONNAME
                if (i == 0 && (sectorData.dataSegments.Count < 2 || !sectorData.data.StartsWith(RegionParser.regionNameDeclaration)))
                {
                    expectingColourDefinition = true;
                    this.eventLogger.AddEvent(
                        new SyntaxError("Invalid first line of region " + data.lines[i], data.fullPath, i + 1)
                    );
                    return;
                }

                if (expectingColourDefinition && sectorData.dataSegments.Count != 3)
                {
                    this.eventLogger.AddEvent(
                        new SyntaxError("Unexpected colour defintion " + data.lines[i], data.fullPath, i + 1)
                    );
                    return;
                }

                // Expecting a colour definition for the region
                if (expectingColourDefinition) {
                    if (sectorData.dataSegments.Count != 3)
                    {
                        this.eventLogger.AddEvent(
                            new SyntaxError("All regions must have a colour " + data.lines[i], data.fullPath, i + 1)
                        );
                        return;
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
                    expectingColourDefinition = false;
                    continue;
                }


                /**
                 * We've found a new REGIONNAME declaration, so must be the start of a new region.
                 */
                if (sectorData.data.StartsWith(RegionParser.regionNameDeclaration))
                {
                    // If it's not the first in the data stream, then it's a new declaration, save the previous
                    if (foundFirst)
                    {
                        this.elements.Add(
                            new Region(
                                regionName,
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

                    regionName = string.Join(" ", sectorData.dataSegments.Skip(1));

                    // Immediately after REGIONNAME, we should expect a colour defition
                    expectingColourDefinition = true;
                    continue;
                }

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

            // We shouldn't end without a fully defined region
            if (expectingColourDefinition)
            {
                this.eventLogger.AddEvent(
                    new SyntaxError(
                        "Incomplete region at end of file: " + data.lines[^1],
                        data.fullPath,
                        data.lines.Count
                    )
                );
                return;
            }

            // Add the last element
            this.elements.Regions.Add(
                new Region(
                    regionName,
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
