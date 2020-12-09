using System.IO;

namespace Compiler.Output
{
    public class EseOutput : AbstractOutputFile
    {
        public EseOutput(TextWriter outputFile)
            : base(outputFile)
        { 
        }

        public override OutputSections[] GetOutputSections()
        {
            return new OutputSections[] {
                OutputSections.ESE_HEADER,
                OutputSections.ESE_POSITIONS,
                OutputSections.ESE_FREETEXT,
                OutputSections.ESE_SIDSSTARS,
                OutputSections.ESE_AIRSPACE,
            };
        }
    }
}
