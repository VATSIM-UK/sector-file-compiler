using System.Collections.Generic;
using System.Linq;
using Compiler.Error;
using Compiler.Event;
using Compiler.Input;
using Compiler.Model;

namespace Compiler.Parser
{
    public class RadarHoleParser: ISectorDataParser
    {
        private readonly SectorElementCollection elements;
        private readonly IEventLogger eventLogger;

        public RadarHoleParser(
            SectorElementCollection elements,
            IEventLogger eventLogger
        ) {
            this.elements = elements;
            this.eventLogger = eventLogger;
        }
        
        public void ParseData(AbstractSectorDataFile data)
        {
            bool foundFirst = false;
            List<SectorData> lines = new();
            foreach (SectorData line in data)
            {
                if (!foundFirst)
                {
                    lines.Add(line);
                    foundFirst = true;
                    continue;
                }

                if (IsNewDeclaration(line))
                {
                    ProcessLines(lines);
                    lines.Clear();
                    lines.Add(line);
                    continue;
                }
                
                lines.Add(line);
            }

            if (lines.Any())
            {
                ProcessLines(lines);
            }
        }

        private bool IsNewDeclaration(SectorData line)
        {
            return line.dataSegments.Count > 0 && line.dataSegments[0] == "HOLE";
        }

        private void ProcessLines(List<SectorData> lines)
        {
            SectorData declarationLine = lines[0];
            if (!IsNewDeclaration(declarationLine))
            {
                eventLogger.AddEvent(
                    new SyntaxError("Radar holes must start with a HOLE declaration", declarationLine)
                );
                return;
            }

            if (declarationLine.dataSegments.Count != 4)
            {
                eventLogger.AddEvent(
                    new SyntaxError("Invalid number of radar HOLE segments", declarationLine)
                );
                return;
            }

            if (declarationLine.dataSegments.GetRange(1, 3).All(segment => segment == ""))
            {
                eventLogger.AddEvent(
                    new SyntaxError("At least one top parameter must be provided for radar HOLE segments", declarationLine)
                );
                return;
            }

            // Get the top values
            int? primaryModeTop = GetModeValue(declarationLine, "primary", declarationLine.dataSegments[1]);
            int? sModeTop = GetModeValue(declarationLine, "s-mode", declarationLine.dataSegments[2]);
            int? cModeTop = GetModeValue(declarationLine, "c-mode", declarationLine.dataSegments[3]);
            if (primaryModeTop == -999 || sModeTop == -999 || cModeTop == -999)
            {
                return;
            }

            // Parse the coordinates
            List<RadarHoleCoordinate> coordinates = new();
            for (int i = 1; i < lines.Count; i++)
            {
                SectorData line = lines[i];
                // Check segment count
                if (line.dataSegments.Count != 3)
                {
                    eventLogger.AddEvent(
                        new SyntaxError("Invalid number of radar HOLE coordinate segments", line)
                    );
                    return;
                }

                // Check COORD
                if (line.dataSegments[0] != "COORD")
                {
                    eventLogger.AddEvent(
                        new SyntaxError("Radar HOLE coordinates must begin with COORD", line)
                    );
                    return;
                }

                // Check the coordinate itself
                if (
                    !CoordinateParser.TryParse(
                        line.dataSegments[1],
                        line.dataSegments[2],
                        out Coordinate parsedCoordinate
                    )
                )
                {
                    eventLogger.AddEvent(
                        new SyntaxError("Invalid radar HOLE coordinate", line)
                    );
                    return;
                }

                // Add the coordinate
                coordinates.Add(
                    new RadarHoleCoordinate(
                        parsedCoordinate,
                        line.definition,
                        line.docblock,
                        line.inlineComment
                    )
                );
            }

            if (coordinates.Count < 3)
            {
                eventLogger.AddEvent(
                    new SyntaxError("Radar HOLE must have at least three coordinates", declarationLine)
                );
                return;
            }
            
            // Create the radar hole
            elements.Add(
                new RadarHole(
                    primaryModeTop,
                    sModeTop,
                    cModeTop,
                    coordinates,
                    declarationLine.definition,
                    declarationLine.docblock,
                    declarationLine.inlineComment
                )
            );
        }

        /*
         * Get the appropriate top value for the given mode.
         */
        private int? GetModeValue(SectorData line, string type, string value)
        {
            if (value == "")
            {
                return null;
            }

            if (!int.TryParse(value, out int parsedValue))
            {
                eventLogger.AddEvent(
                    new SyntaxError($"Invalid {type} top for radar HOLE", line)
                );
                return -999;
            }

            return parsedValue;
        }
    }
}
