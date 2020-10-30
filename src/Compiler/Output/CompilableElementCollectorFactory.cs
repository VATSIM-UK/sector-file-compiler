using System;
using System.Collections.Generic;
using System.Text;
using Compiler.Model;

namespace Compiler.Output
{
    public class CompilableElementCollectorFactory
    {
        private readonly SectorElementCollection sectorElements;
        private readonly OutputGroupRepository outputGroups;

        public CompilableElementCollectorFactory(
            SectorElementCollection sectorElements,
            OutputGroupRepository outputGroups
        ) {
            this.sectorElements = sectorElements;
            this.outputGroups = outputGroups;
        }

        public ICompilableElementCollector GetCollectorForOutputSection(OutputSections section)
        {
            switch (section)
            {
                case OutputSections.SCT_REGIONS:
                    return new RegionsCompilableElementCollector(this.sectorElements, this.outputGroups);
                default:
                    break;
            }

            throw new ArgumentException("No element collector for section " + section.ToString());
        }
    }
}
