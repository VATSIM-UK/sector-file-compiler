using System.IO;

namespace Compiler.Output
{
    /*
     * Represents a possible output file (SCT, ESE, RWY) and defines
     * the sections that are present in each and the order that they come out in.
     */
    public abstract class AbstractOutputFile
    {
        private readonly TextWriter outputStream;

        public AbstractOutputFile(TextWriter outputStream)
        {
            this.outputStream = outputStream;
        }

        public abstract OutputSectionKeys[] GetOutputSections();

        public TextWriter GetOutputStream()
        {
            return this.outputStream;
        }
    }
}
