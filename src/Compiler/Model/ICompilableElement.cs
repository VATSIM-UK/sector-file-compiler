using Compiler.Output;

namespace Compiler.Model
{
    /*
     * An interface that represents a single compilable element spanning one line in the
     * output. For example, a single ARRAPT line.
     */
    public interface ICompilableElement: IDefinable
    {
        public void Compile(SectorElementCollection elements, IOutputWriter output);
    }
}
