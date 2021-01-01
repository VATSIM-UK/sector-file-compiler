﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            Console.WriteLine($"Starting file {outputFile.GetType()}");
            Stopwatch timer = Stopwatch.StartNew();
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
                Stopwatch sectionTimer = Stopwatch.StartNew();
                IEnumerable<ICompilableElementProvider> elementProviders = this.collectorFactory.GetCollectorForOutputSection(section)
                    .GetCompilableElements();
                sectionTimer.Stop();
                Console.WriteLine($"Collection time for section {Enum.GetName(typeof(OutputSectionKeys), section)}: {sectionTimer.ElapsedMilliseconds}ms");
                OutputGroup currentDataGroup = new OutputGroup("INITIAL");

                Stopwatch sectionWriteTimer = Stopwatch.StartNew();
                foreach (ICompilableElementProvider provider in elementProviders)
                {
                    foreach (ICompilableElement element in provider.GetCompilableElements())
                    {
                        // TODO: This bit is _really_ slow, we should think about how we can improve performance
                        if (
                            sectionConfig.printDataGroupings &&
                            outputGroups.TryGetForDefinitionFile(element.GetDefinition(), out OutputGroup group) &&
                            !group.Equals(currentDataGroup)
                        ) {
                            currentDataGroup = group;
                            outputStream.WriteLine(new Comment(group.HeaderDescription));
                        }

                        element.Compile(sectorElements, outputStream);
                    }
                }
                sectionWriteTimer.Stop();
                Console.WriteLine($"Write time for section {Enum.GetName(typeof(OutputSectionKeys), section)}: {sectionWriteTimer.ElapsedMilliseconds}ms");

                // Write a newline at the end of a new section
                outputStream.WriteLine();
                outputStream.Flush();
            }

            // Flush the file to make sure it's written
            outputStream.Flush();
            timer.Stop();
            Console.WriteLine($"Total time for file {outputFile.GetType()}: {timer.ElapsedMilliseconds}ms");
        }
    }
}
