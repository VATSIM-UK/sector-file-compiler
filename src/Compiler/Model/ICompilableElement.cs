using System.IO;

namespace Compiler.Model
{
    /*
     * An interface that represents a single compilable element spanning one line in the
     * output. For example, a single ARRAPT line.
     */
    public interface ICompilableElement
    {
        public void Compile(SectorElementCollection elements, TextWriter output);
    }
}
