using System.Collections.Generic;

namespace Compiler.Model
{
    public class Header: ICompilableElementProvider, IDefinable
    {
        private readonly Definition definition;
        public List<HeaderLine> Lines { get; }

        public Header(Definition definition, List<HeaderLine> lines)
        {
            this.definition = definition;
            this.Lines = lines;
        }

        public IEnumerable<ICompilableElement> GetCompilableElements()
        {
            return this.Lines;
        }

        public Definition GetDefinition()
        {
            return this.definition;
        }
    }
}
