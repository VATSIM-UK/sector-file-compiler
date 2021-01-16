using System.Collections.Generic;
using Compiler.Model;
using Compiler.Error;
using Compiler.Event;
using Compiler.Input;
using Compiler.Validate;

namespace Compiler.Parser
{
    public class EsePositionParser: ISectorDataParser
    {
        private readonly IFrequencyParser frequencyParser;
        private readonly SectorElementCollection sectorElements;
        private readonly IEventLogger errorLog;
        private readonly PositionOrder order;

        private readonly List<string> allowedTypes = new()
        {
            "OBS",
            "DEL",
            "GND",
            "APP",
            "TWR",
            "CTR",
            "FSS",
            "ATIS",
            "INFO"
        };

        const string NoData = "-";

        public EsePositionParser(
            IFrequencyParser frequencyParser,
            SectorElementCollection sectorElements,
            IEventLogger errorLog,
            PositionOrder order
        ) {
            this.frequencyParser = frequencyParser;
            this.sectorElements = sectorElements;
            this.errorLog = errorLog;
            this.order = order;
        }

        public void ParseData(AbstractSectorDataFile data)
        {
            foreach (SectorData line in data)
            {
                if (line.dataSegments.Count < 7)
                {
                    this.errorLog.AddEvent(
                        new SyntaxError("Incorrect number of ESE position segments", line)
                    );
                    this.errorLog.AddEvent(
                        new ParserSuggestion("Have you remembered to add the two non-used fields?")
                    );
                    continue;
                }

                if (this.frequencyParser.ParseFrequency(line.dataSegments[2]) == null)
                {
                    this.errorLog.AddEvent(
                        new SyntaxError("Invalid RTF frequency " + line.dataSegments[0], line)
                    );
                    continue;
                }

                if (!this.allowedTypes.Contains(line.dataSegments[6]))
                {
                    this.errorLog.AddEvent(
                        new SyntaxError("Unknown Position suffix " + line.dataSegments[0], line)
                    );
                    continue;
                }

                if (line.dataSegments.Count == 10)
                {
                    this.errorLog.AddEvent(
                        new SyntaxError("Squawk range end not specified", line)
                    );
                    continue;
                }

                string squawkRangeStart = "";
                string squawkRangeEnd = "";
                if (line.dataSegments.Count > 9 && line.dataSegments[9] != EsePositionParser.NoData && line.dataSegments[9] != "")
                {

                    if (!SquawkValidator.SquawkValid(line.dataSegments[9])) {
                        this.errorLog.AddEvent(
                            new SyntaxError("Invalid squawk range start " + line.dataSegments[9], line)
                        );
                        continue;
                    }

                    if (!SquawkValidator.SquawkValid(line.dataSegments[10]))
                    {
                        this.errorLog.AddEvent(
                            new SyntaxError("Invalid squawk range end " + line.dataSegments[10], line)
                        );
                        continue;
                    }

                    if (int.Parse(line.dataSegments[10]) < int.Parse(line.dataSegments[9]))
                    {
                        this.errorLog.AddEvent(
                            new SyntaxError("Squawk range end is smaller than start " + line.dataSegments[0], line)
                        );
                        continue;
                    }

                    squawkRangeStart = line.dataSegments[9];
                    squawkRangeEnd = line.dataSegments[10];
                }

                // Coordinates start at position 11
                int coordNumber = 11;

                // Check if there's too many vis centers
                if (line.dataSegments.Count - coordNumber > 8)
                {
                    this.errorLog.AddEvent(
                        new SyntaxError("A maxium of 4 visibility centers may be specified " + line.dataSegments[0], line)
                    );
                    continue;
                }

                bool coordinateError = false;
                List<Coordinate> parsedCoordinates = new List<Coordinate>();
                while (coordNumber < line.dataSegments.Count)
                {
                    // Theres only a latitude left, so unparseable - skip
                    if (coordNumber + 1 == line.dataSegments.Count)
                    {
                        this.errorLog.AddEvent(
                            new SyntaxError("Missing visibility center longitude coordinate " + line.dataSegments[0], line)
                        );
                        coordinateError = true;
                        break;
                    }

                    // Ignore skipped centers
                    if (line.dataSegments[coordNumber] == "" && line.dataSegments[coordNumber + 1] == "")
                    {
                        coordNumber += 2;
                        continue;
                    }

                    Coordinate parsedCoordinate = CoordinateParser.Parse(
                        line.dataSegments[coordNumber],
                        line.dataSegments[coordNumber + 1]
                    );

                    if (parsedCoordinate.Equals(CoordinateParser.InvalidCoordinate))
                    {
                        this.errorLog.AddEvent(
                            new SyntaxError("Invalid visibility center " + line.dataSegments[0], line)
                        );
                        coordinateError = true;
                        break;
                    }

                    parsedCoordinates.Add(parsedCoordinate);
                    coordNumber += 2;
                }

                if (coordinateError)
                {
                    continue;
                }

                // Skip two unused sections
                this.sectorElements.Add(
                    new ControllerPosition(
                        line.dataSegments[0],
                        line.dataSegments[1],
                        line.dataSegments[2],
                        line.dataSegments[3],
                        line.dataSegments[4],
                        line.dataSegments[5],
                        line.dataSegments[6],
                        squawkRangeStart,
                        squawkRangeEnd,
                        parsedCoordinates,
                        this.order,
                        line.definition,
                        line.docblock,
                        line.inlineComment
                    )
                );
            }
        }
    }
}
