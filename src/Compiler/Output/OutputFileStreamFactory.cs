using System.IO;

namespace Compiler.Output
{
    public class OutputFileStreamFactory: IOutputStreamFactory
    {
        public TextWriter Make(string file)
        {
            return new StreamWriter(file);
        }
    }
}