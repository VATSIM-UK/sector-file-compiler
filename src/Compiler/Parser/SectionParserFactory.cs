using Compiler.Output;
using Compiler.Event;

namespace Compiler.Parser
{
    public class SectionParserFactory
    {
        private readonly IEventLogger logger;
        public SectionParserFactory(IEventLogger logger)
        {
            this.logger = logger;
        }

        public ISectorElementParser GetParserForSection(OutputSections section)
        {
            switch (section)
            {
                case OutputSections.SCT_ARTCC_LOW:
                    return new ColourParser(this.logger);
                case OutputSections.ESE_SIDSSTARS:
                    return new SidStarParser(this.logger);
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
    }
}
