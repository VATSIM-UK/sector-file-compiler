using System;
using System.Linq;
using Compiler.Error;
using Compiler.Event;
using Compiler.Input;
using Compiler.Model;

namespace Compiler.Parser
{
    public class RadarParser: ISectorDataParser
    {
        private readonly SectorElementCollection elements;
        private readonly IEventLogger eventLogger;

        public RadarParser(
            SectorElementCollection elements,
            IEventLogger eventLogger
        ) {
            this.elements = elements;
            this.eventLogger = eventLogger;
        }
        
        public void ParseData(AbstractSectorDataFile data)
        {
            foreach (SectorData line in data)
            {
                if (line.dataSegments.Count != 13)
                {
                    eventLogger.AddEvent(
                        new SyntaxError("Incorrect number of RADAR2 segments", line)
                    );
                    continue;
                }

                if (line.dataSegments[0] != "RADAR2")
                {
                    eventLogger.AddEvent(
                        new SyntaxError("All radar lines must begin with RADAR2", line)
                    );
                    continue;
                }
                
                // Check the coordinates
                if (
                    !CoordinateParser.TryParse(
                        line.dataSegments[2],
                        line.dataSegments[3],
                        out Coordinate parsedCoordinate)
                    )
                {
                    eventLogger.AddEvent(
                        new SyntaxError("Invalid coordinate in RADAR2 definition", line)
                    );
                    continue;
                }
                
                // Check at at least one of the radar types have parameters
                bool dataProvided = line.dataSegments.GetRange(4, 9)
                    .Any(segment => segment != "");
                if (!dataProvided)
                {
                    eventLogger.AddEvent(
                        new SyntaxError("At least one type of RADAR2 data must be provided", line)
                    );
                    continue;
                }

                // Parse the parameters
                try
                {
                    elements.Add(
                        new Radar(
                            line.dataSegments[1],
                            parsedCoordinate,
                            ParseRadarParameters(
                                "primary",
                                line.dataSegments[4],
                                line.dataSegments[5],
                                line.dataSegments[6]
                            ),
                            ParseRadarParameters(
                                "primary",
                                line.dataSegments[7],
                                line.dataSegments[8],
                                line.dataSegments[9]
                            ),
                            ParseRadarParameters(
                                "primary",
                                line.dataSegments[10],
                                line.dataSegments[11],
                                line.dataSegments[12]
                            ),
                            line.definition,
                            line.docblock,
                            line.inlineComment
                        )
                    );
                }
                catch (ArgumentException exception)
                {
                    eventLogger.AddEvent(
                        new SyntaxError(exception.Message, line)
                    );
                }
            }
        }

        private RadarParameters ParseRadarParameters(string type, string range, string altitude, string coneSlope)
        {
            /*
             * Radars can either provide all three required parameters, or none.
             */
            int numberOfParameters = CheckNumberOfParametersProvided(range, altitude, coneSlope);
            if (numberOfParameters == 0)
            {
                return new RadarParameters();
            }

            if (numberOfParameters != 3)
            {
                throw new ArgumentException("RADAR2 segments must provide all three parameters, or none at all");
            }

            // Check the data that's been passed in as parameters
            if (!int.TryParse(range, out int rangeInt))
            {
                throw new ArgumentException($"Invalid {type} range in RADAR2 segment");
            }
            
            if (!int.TryParse(altitude, out int altitudeInt))
            {
                throw new ArgumentException($"Invalid {type} altitude in RADAR2 segment");
            }
            
            if (!int.TryParse(coneSlope, out int coneSlopeInt))
            {
                throw new ArgumentException($"Invalid {type} cone slope in RADAR2 segment");
            }

            return new RadarParameters(rangeInt, altitudeInt, coneSlopeInt);
        }

        /**
         * Check for empty parameters and return the number provided that have data.
         */
        private int CheckNumberOfParametersProvided(string range, string altitude, string coneSlope)
        {
            int number = 0;
            if (range != "")
            {
                number++;
            }
            
            if (altitude != "")
            {
                number++;
            }
            
            if (coneSlope != "")
            {
                number++;
            }

            return number;
        }
    }
}
