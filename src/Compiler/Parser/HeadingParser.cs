namespace Compiler.Parser
{
    public static class HeadingParser
    {
        public static bool TryParse(string heading, out int parsedHeading)
        {
            if (
                int.TryParse(heading, out int headingInt) &&
                headingInt <= 360 &&
                headingInt > 0
            )
            {
                parsedHeading = headingInt;
                return true;
            }

            parsedHeading = -1;
            return false;
        }
    }
}