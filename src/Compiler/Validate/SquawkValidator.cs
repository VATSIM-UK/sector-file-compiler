using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Compiler.Validate
{
    /*
     * Validates that squawk codes are valid
     */
    public class SquawkValidator
    {
        private static readonly Regex squawkRegex = new Regex(@"^[0-7]{4}$");

        public static bool SquawkValid(string squawk)
        {
            return squawkRegex.IsMatch(squawk);
        }
    }
}
