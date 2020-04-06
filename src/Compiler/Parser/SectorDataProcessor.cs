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
                AbstractSectorElementParser parser = sectionParsers.GetParserForSection(section);
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
                            new CommentLine("Start of input file " + file.GetPath()),
                            section
                        );
                    }

                    SectorFormatData data = InputLineReader.ReadInputLines(file);

                    if (data.Equals(InputLineReader.invalidData))
                    {
                        errors.AddEvent(
                            new FileNotFoundError(file.GetPath())
                        );
                        continue;
                    }

                    parser.ParseData(InputLineReader.ReadInputLines(file)); 
                }
            }
        }
    }
}
