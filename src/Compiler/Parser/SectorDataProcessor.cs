using System;
using Compiler.Output;
using Compiler.Input;
using Compiler.Error;
using Compiler.Event;

namespace Compiler.Parser
{
    public class SectorDataProcessor
    {
        public static void Parse(SectionParserFactory sectionParsers, FileIndex files, IEventLogger errors)
        {
            foreach (OutputSections section in Enum.GetValues(typeof(OutputSections)))
            {
                AbstractSectorElementParser parser = sectionParsers.GetParserForSection(section);
                if (parser == null)
                {
                    errors.AddEvent(
                        new UnconfiguredConfigSectionWarning(section.ToString())
                    );
                }

                foreach (IFileInterface file in files.GetFilesForSection(section))
                {
                    parser.ParseData(InputLineReader.ReadInputLines(file));
                }
            }
        }
    }
}
