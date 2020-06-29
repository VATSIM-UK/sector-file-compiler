using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.Parser
{
    public class SctLabelLineParser : ISectorLineParser
    {
        public SectorFormatLine ParseLine(string line)
        {
            string comment = LineCommentParser.ParseComment(line);
            string data = LineCommentParser.ParseData(line).TrimStart();

            // Find the quotes
            if (data[0] != '"')
            {
                return new SectorFormatLine(
                    "",
                    new List<string>(),
                    null
                );
            }

            int secondQuote = data.IndexOf('"', 1);
            if (secondQuote == -1)
            {
                return new SectorFormatLine(
                    "",
                    new List<string>(),
                    null
                );
            }

            string label = data[1..secondQuote];
            List<string> dataSplit = new List<string>(data.Substring(secondQuote + 1).Split(null));
            dataSplit.RemoveAll(s => s == "");
            dataSplit.Insert(0, label);

            return new SectorFormatLine(
                data,
                dataSplit,
                comment
            );
        }
    }
}
