using System.Text.RegularExpressions;

namespace Compiler.Validate
{
    /*
     * Validates that squawk codes are valid
     */
    public class SquawkValidator
    {
        private static readonly Regex SquawkRegex = new(@"^[0-7]{4}$");

        public static bool SquawkValid(string squawk)
        {
            return SquawkRegex.IsMatch(squawk);
        }
    }
}
