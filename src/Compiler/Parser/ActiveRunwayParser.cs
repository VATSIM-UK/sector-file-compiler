using System;
using System.Collections.Generic;
using Compiler.Event;
using Compiler.Error;
using Compiler.Model;
using Compiler.Validate;
using Compiler.Input;

namespace Compiler.Parser
{
    public class ActiveRunwayParser : AbstractSectorElementParser, ISectorDataParser
    {
        private readonly ISectorLineParser sectorLineParser;
        private readonly SectorElementCollection elements;
        private readonly IEventLogger eventLogger;

        public ActiveRunwayParser(
            MetadataParser metadataParser,
            ISectorLineParser sectorLineParser,
            SectorElementCollection elements,
            IEventLogger eventLogger
        ) : base(metadataParser)
        {
            this.sectorLineParser = sectorLineParser;
            this.elements = elements;
            this.eventLogger = eventLogger;
        }

        public void ParseData(AbstractSectorDataFile data)
        {
            // Loop the lines to get the data out
            foreach (string line in data)
            {
                if (this.ParseMetadata(line))
                {
                    continue;
                }

                SectorFormatLine parsedLine = this.sectorLineParser.ParseLine(line) ;

                if (parsedLine.dataSegments.Count != 4)
                {
                    this.eventLogger.AddEvent(
                        new SyntaxError("Invalid number of ACTIVE_RUNWAY segments", data.FullPath, data.CurrentLineNumber)
                    );
                    return;
                }

                if (parsedLine.dataSegments[0] != "ACTIVE_RUNWAY")
                {
                    this.eventLogger.AddEvent(
                        new SyntaxError("Active runway declarations must begin with ACTIVE_RUNWAY", data.FullPath, data.CurrentLineNumber)
                    );
                    return;
                }

                if (!AirportValidator.IcaoValid(parsedLine.dataSegments[1]))
                {
                    this.eventLogger.AddEvent(
                        new SyntaxError("Invalid airport ICAO for Active runway declaration", data.FullPath, data.CurrentLineNumber)
                    );
                    return;
                }

                if (!RunwayValidator.RunwayValid(parsedLine.dataSegments[2]))
                {
                    this.eventLogger.AddEvent(
                        new SyntaxError("Invalid runway designator for Active runway declaration", data.FullPath, data.CurrentLineNumber)
                    );
                    return;
                }

                if (!int.TryParse(parsedLine.dataSegments[3], out int mode) || (mode != 0 && mode != 1))
                {
                    this.eventLogger.AddEvent(
                        new SyntaxError("Invalid mode for Active runway declaration", data.FullPath, data.CurrentLineNumber)
                    );
                    this.eventLogger.AddEvent(
                        new ParserSuggestion("Valid modes for Active runways are 0 and 1")
                    );
                    return;
                }

                this.elements.Add(
                    new ActiveRunway(
                        parsedLine.dataSegments[2],
                        parsedLine.dataSegments[1],
                        mode,
                        parsedLine.comment
                    )
                );
            }
        }
    }
}
