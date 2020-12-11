using System.Collections.Generic;

namespace Compiler.Model
{
    public class Header: ICompilableElementProvider, IDefinable
    {
        private readonly Definition definition;
        private readonly List<HeaderLine> lines;

        public Header(Definition definition, List<HeaderLine> lines)
        {
            this.definition = definition;
            this.lines = lines;
        }

        public IEnumerable<ICompilableElement> GetCompilableElements()
        {
            return this.lines;
        }

        public Definition GetDefinition()
        {
            return this.definition;
        }
    }
}
