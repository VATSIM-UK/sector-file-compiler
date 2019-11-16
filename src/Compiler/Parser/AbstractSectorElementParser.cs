namespace Compiler.Parser
{
    abstract public class AbstractSectorElementParser
    {
        private readonly MetadataParser metadataParser;

        public AbstractSectorElementParser(MetadataParser metadataParser)
        {
            this.metadataParser = metadataParser;
        }

        /**
         * Check the line for metadata only.
         */
        public bool ParseMetadata(string line)
        {
            return metadataParser.ParseBlankLine(line) || metadataParser.ParseCommentLine(line);
        }

        /**
         * Parse data lines, from a given starting line.
         */
        abstract public void ParseData(SectorFormatData data);
    }
}
