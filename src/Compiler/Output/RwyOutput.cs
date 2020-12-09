using System.IO;

namespace Compiler.Output
{
    public class RwyOutput : AbstractOutputFile
    {
        public RwyOutput(TextWriter outputFile)
            : base(outputFile)
        { 
        
        }

        public override OutputSections[] GetOutputSections()
        {
            return new OutputSections[] {
                OutputSections.RWY_ACTIVE_RUNWAYS
            };
        }
    }
}
