using System.IO;
using Compiler.Output;

namespace CompilerTest.Bogus.Factory
{
    public class MockOutputStreamFactory: IOutputStreamFactory
    {
        private readonly TextWriter writer;

        public MockOutputStreamFactory(TextWriter writer)
        {
            this.writer = writer;
        }
        
        public TextWriter Make(string file)
        {
            return writer;
        }
    }
}