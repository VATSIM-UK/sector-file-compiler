namespace Compiler.Parser
{
    public struct SectorFormatLine
    {
        public readonly string data;
        public readonly string comment;

        public SectorFormatLine(string data, string comment)
        {
            this.data = data;
            this.comment = comment;
        }
    }
}
