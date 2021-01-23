namespace Compiler.Parser
{
    public class EuroscopeNoFrequencyParser : IFrequencyParser
    {
        private const string NoFrequency = "199.998";

        public string ParseFrequency(string frequency)
        {
            return frequency == NoFrequency ? frequency : null;
        }
    }
}
