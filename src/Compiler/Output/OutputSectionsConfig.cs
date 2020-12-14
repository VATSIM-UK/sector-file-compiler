namespace Compiler.Output
{
    public class OutputSectionsConfig
    {
        public static readonly OutputSection[] sections =
        {
            new OutputSection(
                OutputSectionKeys.FILE_HEADER,
                false,
                null
            ),
            new OutputSection(
                OutputSectionKeys.SCT_COLOUR_DEFS,
                false,
                null
            ),
            new OutputSection(
                OutputSectionKeys.SCT_INFO,
                false,
                "[INFO]"
            ),
            new OutputSection(
                OutputSectionKeys.SCT_AIRPORT,
                false,
                "[AIRPORT]"
            ),
            new OutputSection(
                OutputSectionKeys.SCT_RUNWAY,
                false,
                "[RUNWAY]"
            ),
            new OutputSection(
                OutputSectionKeys.SCT_VOR,
                false,
                "[VOR]"
            ),
            new OutputSection(
                OutputSectionKeys.SCT_NDB,
                false,
                "[NDB]"
            ),
            new OutputSection(
                OutputSectionKeys.SCT_FIXES,
                false,
                "[FIXES]"
            ),
            new OutputSection(
                OutputSectionKeys.SCT_GEO,
                true,
                "[GEO]"
            ),
            new OutputSection(
                OutputSectionKeys.SCT_LOW_AIRWAY,
                false,
                "[LOW AIRWAY]"
            ),
            new OutputSection(
                OutputSectionKeys.SCT_HIGH_AIRWAY,
                false,
                "[HIGH AIRWAY]"
            ),
            new OutputSection(
                OutputSectionKeys.SCT_ARTCC,
                false,
                "[ARTCC]"
            ),
            new OutputSection(
                OutputSectionKeys.SCT_ARTCC_HIGH,
                false,
                "[ARTCC HIGH]"
            ),
            new OutputSection(
                OutputSectionKeys.SCT_ARTCC_LOW,
                false,
                "[ARTCC LOW]"
            ),
            new OutputSection(
                OutputSectionKeys.SCT_SID,
                false,
                "[SID]"
            ),
            new OutputSection(
                OutputSectionKeys.SCT_STAR,
                false,
                "[STAR]"
            ),
            new OutputSection(
                OutputSectionKeys.SCT_LABELS,
                true,
                "[LABELS]"
            ),
            new OutputSection(
                OutputSectionKeys.SCT_REGIONS,
                true,
                "[REGIONS]"
            ),
            new OutputSection(
                OutputSectionKeys.ESE_POSITIONS,
                false,
                "[POSITIONS]"
            ),
            new OutputSection(
                OutputSectionKeys.ESE_FREETEXT,
                false,
                "[FREETEXT]"
            ),
            new OutputSection(
                OutputSectionKeys.ESE_SIDSSTARS,
                true,
                "[SIDSSTARS]"
            ),
            new OutputSection(
                OutputSectionKeys.ESE_AIRSPACE,
                true,
                "[AIRSPACE]"
            ),
            new OutputSection(
                OutputSectionKeys.RWY_ACTIVE_RUNWAYS,
                false,
                null
            ),
        };
    }
}
