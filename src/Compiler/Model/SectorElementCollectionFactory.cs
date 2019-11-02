using System;
using Compiler.Output;
using Compiler.Parser;
using Compiler.Input;
using Compiler.Error;
using Compiler.Event;

namespace Compiler.Model
{
    public class SectorElementCollectionFactory
    {
        public static SectorElementCollection Create(SectionParserFactory sectionParsers, FileIndex files, IEventLogger errors)
        {
            SectorElementCollection sectorElements = new SectorElementCollection();
            foreach (OutputSections section in Enum.GetValues(typeof(OutputSections)))
            {
                ISectorElementParser parser = sectionParsers.GetParserForSection(section);
                if (parser == null)
                {
                    errors.AddEvent(
                        new UnconfiguredConfigSectionWarning(section.ToString())
                    );
                    continue;
                }

                foreach (IFileInterface file in files.GetFilesForSection(section))
                {
                    parser.ParseElements(file.GetPath(), PeripheralDataStripper.StripPeripheralData(file.GetAllLines()), sectorElements);
                }
            }

            return sectorElements;
        }
    }
}
