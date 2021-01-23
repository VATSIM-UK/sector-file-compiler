using System.IO;

namespace Compiler.Output
{
    public interface IOutputStreamFactory
    {
        /**
         * Returns an output stream
         */
        public TextWriter Make(string file);
    }
}