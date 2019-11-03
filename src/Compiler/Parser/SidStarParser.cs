using System.Collections.Generic;
using Compiler.Model;
using Compiler.Error;
using Compiler.Event;

namespace Compiler.Parser
{
    public class SidStarParser: ISectorElementParser
    {
        private readonly IEventLogger errorLog;

        public SidStarParser(IEventLogger errorLog)
        {
            this.errorLog = errorLog;
        }

        public void ParseElements(string filename, List<string> lines, SectorElementCollection sectorElements)
        {
            for (int i = 0; i < lines.Count; i++)
            {
                string[] parts = lines[i].Split(':');

                if (parts.Length != 5)
                {
                    this.errorLog.AddEvent(
                        new SyntaxError("Incorrect number of SIDSTAR segments", filename, i)
                    ); ;
                    continue;
                }

                if (parts[0] != "SID" && parts[0] != "STAR")
                {
                    this.errorLog.AddEvent(
                        new SyntaxError("Unknown SIDSTAR type " + parts[0], filename, i)
                    ); ;
                    continue;
                }

                sectorElements.Add(
                    new SidStar(parts[0], parts[1], parts[2], parts[3], new List<string>(parts[4].Split(' ')))
                );
            }
        }
    }
}
