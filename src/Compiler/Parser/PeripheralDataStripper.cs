using System.Collections.Generic;

namespace Compiler.Parser
{
    public class PeripheralDataStripper
    {
        public static List<string> StripPeripheralData(List<string> lines)
        {
            List<string> linesToKeep = new List<string>();

            foreach (string line in lines)
            {
                // Blank line
                if (line.Trim() == "")
                {
                    continue;
                }

                // Comment line
                if (line.Trim().StartsWith(';'))
                {
                    continue;
                }

                // Strip off comments
                int commentPos = line.IndexOf(';');
                linesToKeep.Add(commentPos == -1 ? line.Trim() : line.Substring(0, commentPos).Trim());
            }

            return linesToKeep;
        }
    }
}
