using System.Collections.Generic;
using System.IO;
using Compiler.Model;
using System.Linq;

namespace Compiler.Output
{
    public class OutputGenerator
    {
        private readonly SectorElementCollection sectorElements;
        private readonly OutputGroupRepository outputGroups;
        private readonly CompilableElementCollectorFactory collectorFactory;

        public OutputGenerator(
            SectorElementCollection sectorElements,
            OutputGroupRepository outputGroups,
            CompilableElementCollectorFactory collectorFactory
        ) {
            this.sectorElements = sectorElements;
            this.outputGroups = outputGroups;
            this.collectorFactory = collectorFactory;
        }

        public void GenerateOutput(AbstractOutputFile outputFile)
        {

            TextWriter outputStream = outputFile.GetOutputStream();

            // Process each section in the output
            foreach (OutputSectionKeys section in outputFile.GetOutputSections())
            {
                OutputSection sectionConfig = OutputSectionsConfig.sections.First(s => s.key == section);

                // If the section has a header declaration, do it
                if (sectionConfig.header != null)
                {
                    outputStream.WriteLine(sectionConfig.header);
                    outputStream.WriteLine("");
                }

                // Get the element providers and compile each element
                IEnumerable<ICompilableElementProvider> elementProviders = this.collectorFactory.GetCollectorForOutputSection(section)
                    .GetCompilableElements();
                OutputGroup currentDataGroup = new OutputGroup("INITIAL");

                foreach (ICompilableElementProvider provider in elementProviders)
                {
                    foreach (ICompilableElement element in provider.GetCompilableElements())
                    {
                        if (
                            sectionConfig.printDataGroupings &&
                            outputGroups.TryGetForDefinitionFile(element.GetDefinition(), out OutputGroup group) &&
                            !group.Equals(currentDataGroup)
                        ) {
                            currentDataGroup = group;
                            outputStream.WriteLine(new Comment($"Start of {@group.HeaderDescription}"));
                        }

                        element.Compile(sectorElements, outputStream);
                    }
                }

                // Write a newline at the end of a new section
                outputStream.WriteLine();
            }

            // Flush the file to make sure it's written
            outputStream.Flush();
        }
    }
}
