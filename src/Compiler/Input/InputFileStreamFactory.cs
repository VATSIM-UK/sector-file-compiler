using System.IO;

namespace Compiler.Input
{
    public class InputFileStreamFactory: IInputStreamFactory
    {
        public TextReader GetStream(string fullPath)
        {
            return new StreamReader(fullPath);
        }
    }
}
