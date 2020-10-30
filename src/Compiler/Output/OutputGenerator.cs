using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Compiler.Model;
using System.Linq;

namespace Compiler.Output
{
    public class OutputGenerator
    {
        public void GenerateOutput(SectorElementCollection sectorElements, OutputGroupRepository repository, TextWriter outputFile)
        {
            // TODO: Remove this test code when ready
            Docblock testDocblock = new Docblock();
            testDocblock.AddLine(new Comment("Line 1"));
            testDocblock.AddLine(new Comment("Line 2"));
            Region testRegion = new Region(
                "TESTREGION",
                new List<RegionPoint>()
                {
                    new RegionPoint(new Point("BNN"), new Definition("test", 1), testDocblock, new Comment("inline"), "red"),
                    new RegionPoint(new Point("LAM"), new Definition("test", 1), testDocblock, new Comment("inline")),
                },
                new Definition("testfile.txt", 0),
                testDocblock,
                new Comment("mainInline")
            );
            Region testRegion2 = new Region(
                "TESTREGION",
                new List<RegionPoint>()
                {
                                new RegionPoint(new Point("BNN"), new Definition("test", 1), testDocblock, new Comment("inline"), "red"),
                                new RegionPoint(new Point("LAM"), new Definition("test", 1), testDocblock, new Comment("inline")),
                },
                new Definition("testfile.txt", 0),
                testDocblock,
                new Comment("mainInline")
            );
            Region testRegion3= new Region(
                "TESTREGION",
                new List<RegionPoint>()
                {
                    new RegionPoint(new Point("BNN"), new Definition("test", 1), testDocblock, new Comment("inline"), "red"),
                    new RegionPoint(new Point("LAM"), new Definition("test", 1), testDocblock, new Comment("inline")),
                },
                new Definition("testfile2.txt", 0),
                testDocblock,
                new Comment("mainInline")
            );
            sectorElements.Add(testRegion);
            sectorElements.Add(testRegion2);
            sectorElements.Add(testRegion3);

            OutputGroup testGroup = new OutputGroup("testkey", "Test header");
            testGroup.AddFile("testfile.txt");
            OutputGroup testGroup2 = new OutputGroup("testkey2", "Test header");
            testGroup2.AddFile("testfile2.txt");
            repository.Add(testGroup);
            repository.Add(testGroup2);

            RegionsCompilableElementCollector collector = new RegionsCompilableElementCollector(sectorElements, repository);
            foreach (IGrouping<OutputGroup, ICompilableElementProvider> outputGroup in collector.GetCompilableElements(OutputSections.SCT_REGIONS))
            {
                if (testGroup.HeaderDescription != null)
                {
                    outputFile.WriteLine("; " + testGroup.HeaderDescription);
                }

                foreach (ICompilableElementProvider elementProvider in outputGroup)
                {
                    foreach (ICompilableElement compilableElement in elementProvider.GetCompilableElements())
                    {
                        compilableElement.Compile(sectorElements, outputFile);
                    }
                }
            }
            outputFile.Flush();
        }
    }
}
