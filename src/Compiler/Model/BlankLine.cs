namespace Compiler.Model
{
    public class BlankLine : ICompilable
    {
        public string Compile()
        {
            return "\r\n";
        }
    }
}
