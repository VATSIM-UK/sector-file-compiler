using Compiler.Model;
using Compiler.Error;
using Compiler.Event;
using Compiler.Input;
using Compiler.Validate;
using System.Linq;

namespace Compiler.Parser
{
    public class RunwayParser: ISectorDataParser
    {
        private readonly SectorElementCollection sectorElements;
        private readonly IEventLogger errorLog;

        public RunwayParser(
            SectorElementCollection sectorElements,
            IEventLogger errorLog
        ) {
            this.sectorElements = sectorElements;
            this.errorLog = errorLog;
        }

        public void ParseData(AbstractSectorDataFile data)
        {
            foreach (SectorData line in data)
            {
                // Runways are weird and have double spaces randomly, so handle it.
                if (line.dataSegments.Count < 8)
                {
                    this.errorLog.AddEvent(
                        new SyntaxError("Too few RUNWAY segments", line)
                    );
                    continue;
                }

                // Check the two identifiers
                if (!RunwayValidator.RunwayValidIncludingAdjacent(line.dataSegments[0]))
                {
                    this.errorLog.AddEvent(
                        new SyntaxError("Invalid runway designator " + line.dataSegments[0], line)
                    );
                    continue;
                }

                if (!RunwayValidator.RunwayValidIncludingAdjacent(line.dataSegments[1]))
                {
                    this.errorLog.AddEvent(
                        new SyntaxError("Invalid runway designator " + line.dataSegments[1], line)
                    );
                    continue;
                }

                // Check the two headings
                if (!this.HeadingIsValid(line.dataSegments[2]))
                {
                    this.errorLog.AddEvent(
                        new SyntaxError("Invalid runway heading " + line.dataSegments[2], line)
                    );
                    continue;
                }

                if (!this.HeadingIsValid(line.dataSegments[3]))
                {
                    this.errorLog.AddEvent(
                        new SyntaxError("Invalid runway heading " + line.dataSegments[3], line)
                    );
                    continue;
                }

                // Check the two coordinates
                Coordinate firstThreshold = CoordinateParser.Parse(line.dataSegments[4], line.dataSegments[5]);
                if (firstThreshold.Equals(CoordinateParser.invalidCoordinate))
                {
                    this.errorLog.AddEvent(
                        new SyntaxError("Invalid runway first threshold ", line)
                    );
                    continue;
                }

                Coordinate reverseThreshold = CoordinateParser.Parse(line.dataSegments[6], line.dataSegments[7]);
                if (reverseThreshold.Equals(CoordinateParser.invalidCoordinate))
                {
                    this.errorLog.AddEvent(
                        new SyntaxError("Invalid runway reverse threshold ", line)
                    );
                    continue;
                }

                // Compile together the airfield description
                string runwayDialogDescription = "";
                if (line.dataSegments.Count > 8)
                {

                    runwayDialogDescription = string.Join(
                        " ",
                        line.dataSegments
                            .Skip(8)
                            .Take(line.dataSegments.Count - 8)
                            .ToList()
                    );
                }


                // Add the element
                this.sectorElements.Add(
                    new Runway(
                        data.GetParentDirectoryName(),
                        line.dataSegments[0],
                        int.Parse(line.dataSegments[2]),
                        firstThreshold,
                        line.dataSegments[1],
                        int.Parse(line.dataSegments[3]),
                        reverseThreshold,
                        line.definition,
                        line.docblock,
                        line.inlineComment
                    )
                );
            }
        }

        private bool HeadingIsValid(string heading)
        {
            return int.TryParse(heading, out int headingInt) &&
                headingInt >= 0 &&
                headingInt < 360;
        }
    }
}
