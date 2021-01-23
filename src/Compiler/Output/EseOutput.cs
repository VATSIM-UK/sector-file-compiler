namespace Compiler.Output
{
    public class EseOutput : AbstractOutputFile
    {
        public EseOutput(IOutputWriter outputFile)
            : base(outputFile)
        { 
        }

        public override OutputSectionKeys[] GetOutputSections()
        {
            return new[] {
                OutputSectionKeys.FILE_HEADER,
                OutputSectionKeys.ESE_POSITIONS,
                OutputSectionKeys.ESE_FREETEXT,
                OutputSectionKeys.ESE_SIDSSTARS,
                OutputSectionKeys.ESE_AIRSPACE,
            };
        }
    }
}
