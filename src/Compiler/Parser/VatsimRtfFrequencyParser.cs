using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.Parser
{
    public class VatsimRtfFrequencyParser : IFrequencyParser
    {
        private readonly int firstMinValue;
        private readonly int firstMaxValue;
        private readonly int secondDividend;

        public VatsimRtfFrequencyParser(int firstMinValue, int firstMaxValue, int secondDividend)
        {
            this.firstMinValue = firstMinValue;
            this.firstMaxValue = firstMaxValue;
            this.secondDividend = secondDividend;
        }

        public string ParseFrequency(string frequency)
        {
            // No frequency, accept this
            if (frequency == "199.998")
            {
                return frequency;
            }

            string[] split = frequency.Split('.');
            if (split.Length != 2)
            {
                return null;
            }

            if (!int.TryParse(split[0], out int first) || first < firstMinValue || first > firstMaxValue)
            {
                return null;
            }

            if (!int.TryParse(split[1], out int second))
            {
                return null;
            }

            if ((second % secondDividend) != 0 && ((second + 5) % secondDividend) != 0)
            {
                return null;
            }

            return frequency;
        }
    }
}
