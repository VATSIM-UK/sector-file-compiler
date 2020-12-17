namespace Compiler.Output
{
    public struct OutputSection
    {
        public readonly OutputSectionKeys key;

        public readonly bool printDataGroupings;

        public readonly string header;

        public OutputSection(OutputSectionKeys key, bool printDataGroupings, string header)
        {
            this.key = key;
            this.printDataGroupings = printDataGroupings;
            this.header = header;
        }
    }
}
