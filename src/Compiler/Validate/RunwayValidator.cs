using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Compiler.Validate
{
    public class RunwayValidator
    {
        private static readonly string RUNWAY_REGEX = "^(0?[1-9]|[1-2]\\d|3[0-6])[LCR]?$";

        public static bool RunwayValid(string runway)
        {
            return Regex.IsMatch(runway, RunwayValidator.RUNWAY_REGEX);
        }

        public static bool RunwayValidIncludingAdjacent(string runway)
        {
            return runway == "00" || Regex.IsMatch(runway, RunwayValidator.RUNWAY_REGEX);
        }
    }
}
