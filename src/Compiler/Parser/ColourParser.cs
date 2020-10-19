using System.Collections.Generic;
using Compiler.Error;
using Compiler.Event;
using Compiler.Model;
using Compiler.Input;

namespace Compiler.Parser
{
    public class ColourParser : ISectorDataParser
    {
        private readonly SectorElementCollection sectorElements;
        private readonly IEventLogger errorLog;

        public ColourParser(
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

                if (line.dataSegments.Count != 3)
                {
                    this.errorLog.AddEvent(
                        new SyntaxError("Invalid number of colour definition segments", line)
                    );
                    continue;
                }

                if (line.dataSegments[0] != "#define")
                {
                    this.errorLog.AddEvent(
                        new SyntaxError("Colour definitions must begin with #define", line)
                    );
                    continue;
                }

                if (!int.TryParse(line.dataSegments[2].Trim(), out int colourValue))
                {
                    this.errorLog.AddEvent(
                        new SyntaxError("Defined colour values must be an integer", line)
                    );
                    continue;
                }

                sectorElements.Add(
                    new Colour(line.dataSegments[1], colourValue, line.definition, line.docblock, line.inlineComment)
                );
            }
        }
    }
}
