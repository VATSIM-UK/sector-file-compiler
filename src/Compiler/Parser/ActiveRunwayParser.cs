using System;
using System.Collections.Generic;
using Compiler.Event;
using Compiler.Error;
using Compiler.Model;
using Compiler.Validate;

namespace Compiler.Parser
{
    public class ActiveRunwayParser : AbstractSectorElementParser, IFileParser
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

        public void ParseData(SectorFormatData data)
        {
            // Loop the lines to get the data out
            for (int i = 0; i < data.lines.Count; i++)
            {
                if (this.ParseMetadata(data.lines[i]))
                {
                    continue;
                }

                SectorFormatLine parsedLine = this.sectorLineParser.ParseLine(data.lines[i]);

                if (parsedLine.dataSegments.Count != 4)
                {
                    this.eventLogger.AddEvent(
                        new SyntaxError("Invalid number of ACTIVE_RUNWAY segments", data.fullPath, i + 1)
                    );
                    return;
                }

                if (parsedLine.dataSegments[0] != "ACTIVE_RUNWAY")
                {
                    this.eventLogger.AddEvent(
                        new SyntaxError("Active runway declarations must begin with ACTIVE_RUNWAY", data.fullPath, i + 1)
                    );
                    return;
                }

                if (!AirportValidator.IcaoValid(parsedLine.dataSegments[1]))
                {
                    this.eventLogger.AddEvent(
                        new SyntaxError("Invalid airport ICAO for Active runway declaration", data.fullPath, i + 1)
                    );
                    return;
                }

                if (!RunwayValidator.RunwayValid(parsedLine.dataSegments[2]))
                {
                    this.eventLogger.AddEvent(
                        new SyntaxError("Invalid runway designator for Active runway declaration", data.fullPath, i + 1)
                    );
                    return;
                }

                if (!int.TryParse(parsedLine.dataSegments[3], out int mode) || (mode != 0 && mode != 1))
                {
                    this.eventLogger.AddEvent(
                        new SyntaxError("Invalid mode for Active runway declaration", data.fullPath, i + 1)
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
