using System.IO;

namespace Compiler.Input
{
    public interface IInputStreamFactory
    {
        public TextReader GetStream(string fullPath);
    }
}
