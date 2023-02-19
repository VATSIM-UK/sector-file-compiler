using System.Collections.Generic;
using Compiler.Model;
using System.Linq;
using System.Threading.Tasks;
using Compiler.Argument;
using Compiler.Collector;

namespace Compiler.Output
{
    public class OutputGenerator
    {
        private readonly SectorElementCollection sectorElements;
        private readonly OutputGroupRepository outputGroups;
        private readonly CompilableElementCollectorFactory collectorFactory;
        private readonly CompilerArguments arguments;

        public OutputGenerator(
            SectorElementCollection sectorElements,
            OutputGroupRepository outputGroups,
            CompilableElementCollectorFactory collectorFactory,
            CompilerArguments arguments
        ) {
            this.sectorElements = sectorElements;
            this.outputGroups = outputGroups;
            this.collectorFactory = collectorFactory;
            this.arguments = arguments;
        }

        public Task GenerateOutput(AbstractOutputFile outputFile)
        {
            return Task.Factory.StartNew(() => CreateOutput(outputFile));
        }
        
        public void CreateOutput(AbstractOutputFile outputFile)
        {
            IOutputWriter outputStream = outputFile.GetOutputStream();

            // Process each section in the output
            foreach (OutputSectionKeys section in outputFile.GetOutputSections())
            {
                OutputSection sectionConfig = OutputSectionsConfig.Sections.First(s => s.key == section);

                // If the section has a header declaration, do it
                if (sectionConfig.header != null)
                {
                    outputStream.WriteLine(sectionConfig.header);
                    outputStream.WriteLine();
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
                            outputStream.WriteLine(new Comment(group.HeaderDescription).ToString());
                        }

                        element.Compile(sectorElements, outputStream);
                    }

                    // Add extra new line between elements if pretty printing is on
                    if (arguments.Pretty == Pretty.PRETTY && ElementCanBePrettyPrinted(provider))
                    {
                        outputStream.WriteLine();
                    }
                }

                // Write a newline at the end of a new section
                outputStream.WriteLine();
                outputStream.Flush();
            }

            // Flush the file to make sure it's written
            outputStream.Flush();
        }
        
        private static bool ElementCanBePrettyPrinted(ICompilableElementProvider provider)
        {
            return provider switch
            {
                Geo => true,
                Sector => true,
                Sectorline => true,
                CircleSectorline => true,
                GroundNetwork => true,
                SidStarRoute => true,
                RadarHole => true,
                Region => true,
                _ => false
            };
        }
    }
}
