using System.Collections.Generic;
using Compiler.Model;
using Compiler.Error;
using Compiler.Event;
using Compiler.Input;
using System.Linq;

namespace Compiler.Parser
{
    public class SidStarRouteParser: AbstractSectorElementParser, ISectorDataParser
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

        public void ParseData(AbstractSectorDataFile data)
        {
            List<SectorFormatLine> linesToProcess = new List<SectorFormatLine>();
            bool foundFirst = false;
            int newSegmentStartLine = 0;
            foreach (string line in data)
            {
                // Defer all metadata lines to the base
                if (this.ParseMetadata(line))
                {
                    continue;
                }

                SectorFormatLine sectorData = this.sectorLineParser.ParseLine(line);

                // We haven't yet started a SID/STAR route, so make sure we have a new declaration
                if (
                    !foundFirst &&
                    (this.GetFirstPointIndex(sectorData) == -1 ||
                    this.GetFirstPointIndex(sectorData) == 0)
                )
                {
                    this.errorLog.AddEvent(
                        new SyntaxError("Invalid new SIDSTAR route declaration", data.FullPath, data.CurrentLineNumber)
                    );
                    return;
                } else if (!foundFirst) {
                    linesToProcess.Add(sectorData);
                    newSegmentStartLine = data.CurrentLineNumber;
                    foundFirst = true;
                    continue;
                }

                // We've reached the end of the segment, time to make the thing and start over!
                int firstPointIndex = this.GetFirstPointIndex(sectorData);
                if (firstPointIndex == -1)
                {
                    this.errorLog.AddEvent(
                      new SyntaxError("Unable to find first point in SID/STAR route", data.FullPath, data.CurrentLineNumber)
                    );
                    this.errorLog.AddEvent(new ParserSuggestion("Have you provided valid coordinates?"));
                    return;
                } else if (firstPointIndex != 0)
                {
                    this.ProcessSidStar(linesToProcess, data, newSegmentStartLine);
                    linesToProcess.Clear();
                    linesToProcess.Add(sectorData);
                    continue;
                }

                // It may be a standard route line with no identifier present (we validate it later)
                linesToProcess.Add(sectorData);
            }

            // If we've got some lines left over, process them now
            if (linesToProcess.Count != 0)
            {
                this.ProcessSidStar(linesToProcess, data, newSegmentStartLine);
            }
        }

        private int GetFirstPointIndex(SectorFormatLine sectorData)
        {
            bool foundSecondPoint = false;
            for (int i = sectorData.dataSegments.Count - 2; i >= 0;)
            {
                // Work til we find the first valid point
                if (!PointParser.Parse(sectorData.dataSegments[i], sectorData.dataSegments[i + 1]).Equals(PointParser.invalidPoint)) {
                    
                    // We've found the second valid point
                    if (!foundSecondPoint)
                    {
                        foundSecondPoint = true;
                        i -= 2;
                        continue;
                    }

                    // Found the first valid point
                    return i;
                }

                i--;
            }

            return -1;
        }

        /**
         * Process an individual SID/STARs worth of lines
         */
        private void ProcessSidStar(List<SectorFormatLine> lines, AbstractSectorDataFile data, int startLine)
        {
            // Get the name out and remove it from the array
            int firstLineFirstCoordinateIndex = this.GetFirstPointIndex(lines[0]);
            if (firstLineFirstCoordinateIndex == -1)
            {
                this.errorLog.AddEvent(
                    new SyntaxError("Invalid SID/STAR route segment coordinates", data.FullPath, startLine)
                );
                return;
            }

            string sidStarName = string.Join(" ", lines[0].dataSegments.GetRange(0, firstLineFirstCoordinateIndex));
            lines[0].dataSegments.RemoveRange(0, firstLineFirstCoordinateIndex);
            List<RouteSegment> segments = new List<RouteSegment>();


            for(int i = 0; i < lines.Count; i++)
            {
                segments.Add(
                    new RouteSegment(
                        PointParser.Parse(lines[i].dataSegments[0], lines[i].dataSegments[1]),
                        PointParser.Parse(lines[i].dataSegments[2], lines[i].dataSegments[3]),
                        lines[i].dataSegments.Count == 5 ? lines[i].dataSegments[4] : null,
                        lines[i].comment
                    )
                );
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
