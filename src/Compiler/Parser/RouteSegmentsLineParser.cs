using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Compiler.Parser
{
    public class RouteSegmentsLineParser : ISectorLineParser
    {
        public SectorFormatLine ParseLine(string line)
        {
            string comment = LineCommentParser.ParseComment(line);
            string data = LineCommentParser.ParseData(line);

            // The first 26 characters in these style lines are all one block
            if (data.Length < 26)
            {
                return new SectorFormatLine(
                    "",
                    new List<string>(),
                    null
                );
            }

            string nameSection = data.Substring(0, 26);
            if (Regex.Replace(nameSection, @"s", "") == "")
            {
                nameSection = "";
            }


            List<string> dataSplit = new List<string>(data.Substring(26).TrimStart().Split(null));
            dataSplit.Insert(0, nameSection.Trim());
            dataSplit.RemoveAll(s => s == "");

            return new SectorFormatLine(
                data,
                dataSplit,
                comment
            );
        }
    }
}
