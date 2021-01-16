using System.Text.RegularExpressions;

namespace Compiler.Validate
{
    /*
     * Validates that squawk codes are valid
     */
    public class SquawkValidator
    {
        private static readonly Regex squawkRegex = new(@"^[0-7]{4}$");

        public static bool SquawkValid(string squawk)
        {
            return squawkRegex.IsMatch(squawk);
        }
    }
}
