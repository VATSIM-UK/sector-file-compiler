using Compiler.Output;
using Compiler.Event;
using Compiler.Model;

namespace Compiler.Parser
{
    public class SectionParserFactory
    {
        private readonly SectorElementCollection sectorElements;
        private readonly IEventLogger logger;

        public SectionParserFactory(SectorElementCollection sectorElements, IEventLogger logger)
        {
            this.sectorElements = sectorElements;
            this.logger = logger;
        }

        public AbstractSectorElementParser GetParserForSection(OutputSections section)
        {
            switch (section)
            {
                case OutputSections.SCT_COLOUR_DEFS:
                    return new ColourParser(this.GetMetadataParser(section), this.sectorElements, this.logger);
                case OutputSections.ESE_SIDSSTARS:
                    return new SidStarParser(this.GetMetadataParser(section), this.sectorElements, this.logger);
                case OutputSections.ESE_PREAMBLE:
                    break;
                case OutputSections.ESE_POSITIONS:
                    break;
                case OutputSections.ESE_FREETEXT:
                    break;
                case OutputSections.ESE_AIRSPACE:
                    break;
            }

            return null;
        }

        private MetadataParser GetMetadataParser(OutputSections section)
        {
            return new MetadataParser(this.sectorElements, section);
        }
    }
}
