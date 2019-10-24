using System.Collections.Generic;
using Compiler.Model;
using Compiler.Error;

namespace Compiler.Parser
{
    public class SidStarParser: ISectorElementParser
    {
        private readonly ErrorLog errorLog;

        public SidStarParser(ErrorLog errorLog)
        {
            this.errorLog = errorLog;
        }

        public void ParseElements(string filename, List<string> lines, SectorElementCollection sectorElements)
        {
            for (int i = 0; i < lines.Count; i++)
            {
                string[] parts = lines[0].Split(':');

                if (parts.Length != 5)
                {
                    this.errorLog.AddError(
                        new CompilerError(ErrorType.SyntaxError, ErrorCode.SidStarSegments, filename, i, "Incorrect number of SIDSTAR segements")
                    );
                    continue;
                }

                if (parts[0] != "SID" && parts[0] != "STAR")
                {
                    this.errorLog.AddError(
                        new CompilerError(ErrorType.SyntaxError, ErrorCode.SidStarType, filename, i, "Unknown SIDSTAR type " + parts[0])
                    );
                    continue;
                }

                sectorElements.AddSidStar(
                    new SidStar(parts[0], parts[1], parts[2], parts[3], new List<string>(parts[4].Split(' ')))
                );
            }
        }
    }
}
