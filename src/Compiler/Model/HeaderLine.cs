using System.IO;

namespace Compiler.Model
{
    public class HeaderLine: ICompilableElement
    {
        private readonly Comment line;
        private readonly Definition definition;

        public HeaderLine(Comment line, Definition definition)
        {
            this.line = line;
            this.definition = definition;
        }

        public Definition GetDefinition()
        {
            return this.definition;
        }

        public void Compile(SectorElementCollection elements, TextWriter output)
        {
            output.WriteLine(line.ToString());
        }
    }
}
