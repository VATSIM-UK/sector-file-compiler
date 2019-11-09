using Compiler.Output;

namespace Compiler.Model
{
    public class SectionHeaderFactory
    {
        /**
         * Create the section headers or a null element if a section doesnt
         * have a header.
         */
        public static ICompilable Create(OutputSections section)
        {
            if (!SectionHeaders.headers.TryGetValue(section, out string header))
            {
                return new NullElement();
            }

            return new SectionHeader(header);
        }
    }
}
