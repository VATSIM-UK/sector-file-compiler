namespace Compiler.Output
{
    public class SctOutput : AbstractOutputFile
    {
        public SctOutput(IOutputWriter outputFile)
            : base(outputFile)
        {

        }

        public override OutputSectionKeys[] GetOutputSections()
        {
            return new[] {
                OutputSectionKeys.FILE_HEADER,
                OutputSectionKeys.SCT_COLOUR_DEFS,
                OutputSectionKeys.SCT_INFO,
                OutputSectionKeys.SCT_AIRPORT,
                OutputSectionKeys.SCT_RUNWAY,
                OutputSectionKeys.SCT_VOR,
                OutputSectionKeys.SCT_NDB,
                OutputSectionKeys.SCT_FIXES,
                OutputSectionKeys.SCT_GEO,
                OutputSectionKeys.SCT_LOW_AIRWAY,
                OutputSectionKeys.SCT_HIGH_AIRWAY,
                OutputSectionKeys.SCT_ARTCC,
                OutputSectionKeys.SCT_ARTCC_HIGH,
                OutputSectionKeys.SCT_ARTCC_LOW,
                OutputSectionKeys.SCT_SID,
                OutputSectionKeys.SCT_STAR,
                OutputSectionKeys.SCT_LABELS,
                OutputSectionKeys.SCT_REGIONS
            };
        }
        
        public override string GetFileDescriptor()
        {
            return "SCT";
        }
    }
}
