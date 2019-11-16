using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.Parser
{
    public class StandardSctLineParser : ISectorLineParser
    {
        public SectorFormatLine ParseLine(string line)
        {
            string comment = LineCommentParser.ParseComment(line);
            string data = LineCommentParser.ParseData(line);

            List<string> dataSplit = new List<string>(data.Split(' '));
            dataSplit.RemoveAll(s => s == "");

            return new SectorFormatLine(
                data,
                dataSplit,
                comment
            );
        }
    }
}
