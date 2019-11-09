namespace Compiler.Model
{
    public class SectionFooter : ICompilable
    {
        public string Compile()
        {
            return "\r\n";
        }
    }
}
