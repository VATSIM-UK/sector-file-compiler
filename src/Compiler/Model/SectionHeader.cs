using System;

namespace Compiler.Model
{
    public class SectionHeader : ICompilable
    {
        private readonly string section;

        public SectionHeader(string section)
        {
            this.section = section;
        }

        public string Compile()
        {
            return String.Format(
                "[{0}]\r\n\r\n",
                this.section
            );
        }
    }
}
