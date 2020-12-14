using System.Collections.Generic;
using System.IO;
using Compiler.Model;
using System.Linq;

namespace Compiler.Output
{
    public class OutputGenerator
    {
        private readonly SectorElementCollection sectorElements;
        private readonly CompilableElementCollectorFactory collectorFactory;

        public OutputGenerator(
            SectorElementCollection sectorElements,
            CompilableElementCollectorFactory collectorFactory
        ) {
            this.sectorElements = sectorElements;
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

                // Get the output groups
                IEnumerable<IGrouping<OutputGroup, ICompilableElementProvider>> outputGroups =
                    this.collectorFactory.GetCollectorForOutputSection(section).GetCompilableElements();

                foreach (IGrouping<OutputGroup, ICompilableElementProvider> outputGroup in outputGroups)
                {
                    // Print the header for the group if there is one
                    if (outputGroup.Key.HeaderDescription != null)
                    {
                        outputStream.WriteLine("; " + outputGroup.Key.HeaderDescription);
                    }

                    // For every individual compilable element (each line of output) print it
                    foreach (ICompilableElementProvider elementProvider in outputGroup)
                    {
                        foreach (ICompilableElement compilableElement in elementProvider.GetCompilableElements())
                        {
                            compilableElement.Compile(sectorElements, outputStream);
                        }
                    }
                }

                // Write a newline
                outputStream.WriteLine();
            }

            // Flush the file to make sure it's written
            outputStream.Flush();
        }
    }
}
