namespace Compiler.Model
{
    /**
     * Represents a compilable element that has no value, it is a placeholder.
     */
    public class NullElement : ICompilable
    {
        public string Compile()
        {
            return "";
        }
    }
}
