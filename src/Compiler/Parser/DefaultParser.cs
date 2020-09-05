using Compiler.Input;

namespace Compiler.Parser
{
    /**
     * A default parser for data that can only parse comments and
     * blank lines.
     */
    public class DefaultParser : AbstractSectorElementParser, ISectorDataParser
    {
        public DefaultParser(MetadataParser metadata) : base(metadata)
        {

        }

        public void ParseData(AbstractSectorDataFile data)
        {
            foreach (string line in data)
            {
                this.ParseMetadata(line);
            }
        }
    }
}
