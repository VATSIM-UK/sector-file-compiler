using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Compiler.Output
{
    public class SubsectionMapper
    {
        private static readonly Dictionary<OutputSections, List<Subsections>> subsections = new Dictionary<OutputSections, List<Subsections>>
        {
            {
                OutputSections.ESE_AIRSPACE,
                new List<Subsections>
                {
                    Subsections.ESE_AIRSPACE_SECTORLINE,
                    Subsections.ESE_AIRSPACE_SECTOR,
                    Subsections.ESE_AIRSPACE_COORDINATION
                }
            }
        };

        public static List<Subsections> GetSubsectionsForSection(OutputSections section)
        {
            return subsections.ContainsKey(section)
                ? subsections[section]
                : new List<Subsections> { Subsections.DEFAULT };
        }
    }
}
