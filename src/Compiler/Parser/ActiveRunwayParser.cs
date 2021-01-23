using Compiler.Event;
using Compiler.Error;
using Compiler.Model;
using Compiler.Validate;
using Compiler.Input;

namespace Compiler.Parser
{
    public class ActiveRunwayParser : ISectorDataParser
    {
        private readonly SectorElementCollection elements;
        private readonly IEventLogger eventLogger;

        public ActiveRunwayParser(
            SectorElementCollection elements,
            IEventLogger eventLogger
        ) {
            this.elements = elements;
            this.eventLogger = eventLogger;
        }

        public void ParseData(AbstractSectorDataFile data)
        {
            // Loop the lines to get the data out
            foreach (SectorData line in data)
            {
                if (line.dataSegments.Count != 4)
                {
                    this.eventLogger.AddEvent(
                        new SyntaxError("Invalid number of ACTIVE_RUNWAY segments", line)
                    );
                    return;
                }

                if (line.dataSegments[0] != "ACTIVE_RUNWAY")
                {
                    this.eventLogger.AddEvent(
                        new SyntaxError("Active runway declarations must begin with ACTIVE_RUNWAY", line)
                    );
                    return;
                }

                if (!AirportValidator.IcaoValid(line.dataSegments[1]))
                {
                    this.eventLogger.AddEvent(
                        new SyntaxError("Invalid airport ICAO for Active runway declaration", line)
                    );
                    return;
                }

                if (!RunwayValidator.RunwayValid(line.dataSegments[2]))
                {
                    this.eventLogger.AddEvent(
                        new SyntaxError("Invalid runway designator for Active runway declaration", line)
                    );
                    return;
                }

                if (!int.TryParse(line.dataSegments[3], out int mode) || (mode != 0 && mode != 1))
                {
                    this.eventLogger.AddEvent(
                        new SyntaxError("Invalid mode for Active runway declaration", line)
                    );
                    this.eventLogger.AddEvent(
                        new ParserSuggestion("Valid modes for Active runways are 0 and 1")
                    );
                    return;
                }

                this.elements.Add(
                    new ActiveRunway(
                        line.dataSegments[2],
                        line.dataSegments[1],
                        mode,
                        line.definition,
                        line.docblock,
                        line.inlineComment
                    )
                );
            }
        }
    }
}
