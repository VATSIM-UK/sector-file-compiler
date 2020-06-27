namespace Compiler.Parser
{
    /*
     * A class containing useful methods for parsing metadata out of
     * data files.
     */
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
    }
}
