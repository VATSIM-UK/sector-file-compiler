using System.Collections.Generic;
using Compiler.Model;
using Compiler.Error;
using Compiler.Event;

namespace Compiler.Parser
{
    public class SidStarRouteParser: AbstractSectorElementParser
    {
        private readonly ISectorLineParser sectorLineParser;
        private readonly SectorElementCollection sectorElements;
        private readonly IEventLogger errorLog;

        public SidStarRouteParser(
            MetadataParser metadataParser,
            ISectorLineParser sectorLineParser,
            SectorElementCollection sectorElements,
            IEventLogger errorLog,
            SidStarType type
        ) : base(metadataParser)
        {
            this.sectorLineParser = sectorLineParser;
            this.sectorElements = sectorElements;
            this.errorLog = errorLog;
            this.Type = type;
        }

        public SidStarType Type { get; }

        public override void ParseData(SectorFormatData data)
        {
            List<SectorFormatLine> linesToProcess = new List<SectorFormatLine>();
            bool foundFirst = false;
            int newSegmentStartLine = 0;
            for (int i = 0; i < data.lines.Count; i++)
            {
                // Defer all metadata lines to the base
                if (this.ParseMetadata(data.lines[i]))
                {
                    continue;
                }

                SectorFormatLine sectorData = this.sectorLineParser.ParseLine(data.lines[i]);

                // We haven't yet started a SID/STAR route, so make sure we have a new declaration
                if (!foundFirst && sectorData.dataSegments.Count != 5)
                {
                    this.errorLog.AddEvent(
                        new SyntaxError("Incorrect number of SIDSTAR route segments, expected new route declaration", data.fullPath, i + 1)
                    );
                    continue;
                } else if (!foundFirst)
                {
                    linesToProcess.Add(sectorData);
                    foundFirst = true;
                    continue;
                }

                // We've reached the end of the segment, time to make the thing and start over!
                if (sectorData.dataSegments.Count == 5)
                {
                    this.processSidStar(linesToProcess, data.fullPath, newSegmentStartLine);
                    linesToProcess.Clear();
                    newSegmentStartLine = i;
                    linesToProcess.Add(sectorData);
                    continue;
                }

                // It may be a standard route line with no identifier present (we validate it later)
                linesToProcess.Add(sectorData);
            }

            // If we've got some lines left over, process them now
            if (linesToProcess.Count != 0)
            {
                this.processSidStar(linesToProcess, data.fullPath, newSegmentStartLine);
            }
        }

        /**
         * Process an individual SID/STARs worth of lines
         */
        private void processSidStar(List<SectorFormatLine> lines, string filename, int startLine)
        {
            string sidStarName = lines[0].dataSegments[0];
            List<RouteSegment> segments = new List<RouteSegment>();
            lines[0].dataSegments.RemoveAt(0);

            Point startPoint;
            Point endPoint;
            for(int i = 0; i < lines.Count; i++)
            {
                startPoint = PointParser.Parse(lines[i].dataSegments[0], lines[i].dataSegments[1]);
                endPoint = PointParser.Parse(lines[i].dataSegments[2], lines[i].dataSegments[3]);

                if (startPoint == PointParser.invalidPoint || endPoint == PointParser.invalidPoint)
                {
                    this.errorLog.AddEvent(
                        new SyntaxError("Invalid route segment", filename, startLine + i + 1)
                    );
                }

                segments.Add(new RouteSegment(startPoint, endPoint, lines[i].comment));
            }

            this.sectorElements.Add(
                new SidStarRoute(
                    this.Type,
                    sidStarName,
                    segments
                )
            );
        }
    }
}
