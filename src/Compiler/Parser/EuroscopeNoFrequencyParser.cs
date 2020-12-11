namespace Compiler.Parser
{
    public class EuroscopeNoFrequencyParser : IFrequencyParser
    {
        private const string NO_FREQUENCY = "199.998";

        public string ParseFrequency(string frequency)
        {
            return frequency == NO_FREQUENCY ? frequency : null;
        }
    }
}
