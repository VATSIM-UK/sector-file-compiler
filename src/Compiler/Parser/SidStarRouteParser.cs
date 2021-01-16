using System.Collections.Generic;
using Compiler.Model;
using Compiler.Error;
using Compiler.Event;
using Compiler.Input;
using System.Linq;

namespace Compiler.Parser
{
    public class SidStarRouteParser: ISectorDataParser
    {
        private readonly SectorElementCollection sectorElements;
        private readonly IEventLogger errorLog;

        public SidStarRouteParser(
            SectorElementCollection sectorElements,
            IEventLogger errorLog,
            SidStarType type
        ) {
            this.sectorElements = sectorElements;
            this.errorLog = errorLog;
            this.Type = type;
        }

        public SidStarType Type { get; }

        public void ParseData(AbstractSectorDataFile data)
        {
            List<SectorData> linesToProcess = new List<SectorData>();
            bool foundFirst = false;
            int newSegmentStartLine = 0;
            foreach (SectorData line in data)
            {
                // We haven't yet started a SID/STAR route, so make sure we have a new declaration
                if (
                    !foundFirst &&
                    (this.GetFirstPointIndex(line) == -1 ||
                    this.GetFirstPointIndex(line) == 0)
                )
                {
                    this.errorLog.AddEvent(
                        new SyntaxError("Invalid new SIDSTAR route declaration", line)
                    );
                    return;
                } else if (!foundFirst) {
                    linesToProcess.Add(line);
                    newSegmentStartLine = data.CurrentLineNumber;
                    foundFirst = true;
                    continue;
                }

                // We've reached the end of the segment, time to make the thing and start over!
                int firstPointIndex = this.GetFirstPointIndex(line);
                if (firstPointIndex == -1)
                {
                    this.errorLog.AddEvent(
                      new SyntaxError("Unable to find first point in SID/STAR route", line)
                    );
                    this.errorLog.AddEvent(new ParserSuggestion("Have you provided valid coordinates?"));
                    return;
                } else if (firstPointIndex != 0)
                {
                    this.ProcessSidStar(linesToProcess, data, newSegmentStartLine);
                    linesToProcess.Clear();
                    linesToProcess.Add(line);
                    continue;
                }

                // It may be a standard route line with no identifier present (we validate it later)
                linesToProcess.Add(line);
            }

            // If we've got some lines left over, process them now
            if (linesToProcess.Count != 0)
            {
                this.ProcessSidStar(linesToProcess, data, newSegmentStartLine);
            }
        }

        private int GetFirstPointIndex(SectorData sectorData)
        {
            bool foundSecondPoint = false;
            for (int i = sectorData.dataSegments.Count - 2; i >= 0;)
            {
                // Work til we find the first valid point
                if (!PointParser.Parse(sectorData.dataSegments[i], sectorData.dataSegments[i + 1]).Equals(PointParser.InvalidPoint)) {
                    
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
        private void ProcessSidStar(List<SectorData> lines, AbstractSectorDataFile data, int startLine)
        {
            // Get the name out and remove it from the array
            int firstLineFirstCoordinateIndex = this.GetFirstPointIndex(lines[0]);
            if (firstLineFirstCoordinateIndex == -1)
            {
                this.errorLog.AddEvent(
                    new SyntaxError("Invalid SID/STAR route segment coordinates", lines[0])
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
                        sidStarName,
                        PointParser.Parse(lines[i].dataSegments[0], lines[i].dataSegments[1]),
                        PointParser.Parse(lines[i].dataSegments[2], lines[i].dataSegments[3]),
                        lines[i].definition,
                        lines[i].docblock,
                        lines[i].inlineComment,
                        lines[i].dataSegments.Count == 5 ? lines[i].dataSegments[4] : null
                    )
                );
            }

            RouteSegment initialSegment = segments.ElementAt(0);
            segments.RemoveAt(0);

            this.sectorElements.Add(
                new SidStarRoute(
                    this.Type,
                    sidStarName,
                    initialSegment,
                    segments,
                    lines[0].definition,
                    lines[0].docblock,
                    lines[0].inlineComment
                )
            );
        }
    }
}
