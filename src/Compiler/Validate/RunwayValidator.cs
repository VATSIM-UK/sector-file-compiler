using System.Text.RegularExpressions;

namespace Compiler.Validate
{
    public class RunwayValidator
    {
        private static readonly string RunwayRegex = "^(0?[1-9]|[1-2]\\d|3[0-6])[LCRG]?$";

        public static bool RunwayValid(string runway)
        {
            return Regex.IsMatch(runway, RunwayRegex);
        }

        public static bool RunwayValidIncludingAdjacent(string runway)
        {
            return runway == "00" || Regex.IsMatch(runway, RunwayRegex);
        }
    }
}
