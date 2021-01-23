namespace Compiler.Parser
{
    public class FrequencyParser : IFrequencyParser
    {
        private readonly int firstMinValue;
        private readonly int firstMaxValue;
        private readonly int secondDividend;

        public FrequencyParser(int firstMinValue, int firstMaxValue, int secondDividend)
        {
            this.firstMinValue = firstMinValue;
            this.firstMaxValue = firstMaxValue;
            this.secondDividend = secondDividend;
        }

        public string ParseFrequency(string frequency)
        {
            string[] split = frequency.Split('.');
            if (split.Length != 2)
            {
                return null;
            }

            if (!int.TryParse(split[0], out int first) || first < firstMinValue || first > firstMaxValue)
            {
                return null;
            }

            if (!int.TryParse(split[1], out int second) || (second % secondDividend) != 0)
            {
                return null;
            }

            return frequency;
        }
    }
}
