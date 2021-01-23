namespace Compiler.Parser
{
    public class VatsimRtfFrequencyParser : IFrequencyParser
    {
        // This has to be much lower because some are defined on the VORs
        const int FirstMinValue = 108;
        const int FirstMaxValue = 136;
        const int SecondDividend = 25;

        private const string PrePositionsFrequency = "199.998";
        private const string NotValidFrequency = "199.900";

        public string ParseFrequency(string frequency)
        {
            // No frequency, accept this
            if (frequency == PrePositionsFrequency || frequency == NotValidFrequency)
            {
                return frequency;
            }

            string[] split = frequency.Split('.');
            if (split.Length != 2)
            {
                return null;
            }

            if (!int.TryParse(split[0], out int first) || first < FirstMinValue || first > FirstMaxValue)
            {
                return null;
            }

            if (!int.TryParse(split[1], out int second))
            {
                return null;
            }

            if ((second % SecondDividend) != 0 && ((second + 5) % SecondDividend) != 0)
            {
                return null;
            }

            return frequency;
        }
    }
}
