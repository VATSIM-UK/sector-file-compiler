using System;
using Compiler.Output;
using Compiler.Input;
using Compiler.Error;
using Compiler.Event;
using Compiler.Model;
using Compiler.Argument;

namespace Compiler.Parser
{
    public class SectorDataProcessor
    {
        public static void Parse(
            SectionParserFactory sectionParsers,
            SectorElementCollection elements,
            CompilerArguments args,
            FileIndex files,
            IEventLogger errors
        ) {
            foreach (OutputSections section in Enum.GetValues(typeof(OutputSections)))
            {
                ISectorDataParser parser = sectionParsers.GetParserForSection(section);
                if (parser == null)
                {
                    errors.AddEvent(
                        new UnconfiguredConfigSectionWarning(section.ToString())
                    );
                }

                foreach (IFileInterface file in files.GetFilesForSection(section))
                {
                    if (args.DisplayInputFiles)
                    {
                        elements.Add(
                            new Comment("Start of input file " + file.GetPath()),
                            section
                        );
                    }

                    parser.ParseData(new SectorDataFile(file.GetPath()));
                }
            }
        }
    }
}
