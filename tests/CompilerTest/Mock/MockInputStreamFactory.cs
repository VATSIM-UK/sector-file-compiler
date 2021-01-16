using System.Collections.Generic;
using System.IO;
using System.Text;
using Compiler.Input;

namespace CompilerTest.Mock
{
    class MockInputStreamFactory: IInputStreamFactory
    {
        private readonly List<string> lines;

        public MockInputStreamFactory(List<string> lines)
        {
            this.lines = lines;
        }

        public TextReader GetStream(string fullPath)
        {
            StringBuilder builder = new();
            foreach (string line in lines)
            {
                builder.AppendLine(line);
            }

            
            return new StringReader(builder.ToString());
        }
    }
}
