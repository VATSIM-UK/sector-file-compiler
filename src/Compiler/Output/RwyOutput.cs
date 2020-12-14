using System.IO;

namespace Compiler.Output
{
    public class RwyOutput : AbstractOutputFile
    {
        public RwyOutput(TextWriter outputFile)
            : base(outputFile)
        { 
        
        }

        public override OutputSectionKeys[] GetOutputSections()
        {
            return new[] {
                OutputSectionKeys.FILE_HEADER,
                OutputSectionKeys.RWY_ACTIVE_RUNWAYS
            };
        }
    }
}
