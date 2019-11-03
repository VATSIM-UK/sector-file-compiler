using System.Collections.Generic;
using Compiler.Error;
using Compiler.Event;
using Compiler.Model;

namespace Compiler.Parser
{
    public class ColourParser : ISectorElementParser
    {
        private readonly IEventLogger errorLog;

        public ColourParser(IEventLogger errorLog)
        {
            this.errorLog = errorLog;
        }

        public void ParseElements(string filename, List<string> lines, SectorElementCollection sectorElements)
        {
            for (int i = 0; i < lines.Count; i++)
            {
                string[] parts = lines[i].Split(" ");

                if (parts.Length != 3)
                {
                    this.errorLog.AddEvent(new SyntaxError("Invalid number of colour definition segments", filename, i));
                    continue;
                }

                if (parts[0] != "#define")
                {
                    this.errorLog.AddEvent(new SyntaxError("Colour definitions must begin with #define", filename, i));
                    continue;
                }

                int colourValue = 0;
                if (!int.TryParse(parts[2], out colourValue))
                {
                    this.errorLog.AddEvent(new SyntaxError("Colour values must be an integer", filename, i));
                    continue;
                }

                sectorElements.Add(new Colour(parts[1], colourValue));
            }
        }
    }
}
