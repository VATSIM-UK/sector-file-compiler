using System.IO;
using Compiler.Transformer;

namespace Compiler.Output
{
    public class OutputWriter: IOutputWriter
    {
        private readonly TransformerChain transformerChain;
        private readonly TextWriter outputStream;

        public OutputWriter(
            TransformerChain transformerChain,
            TextWriter outputStream
        ) {
            this.transformerChain = transformerChain;
            this.outputStream = outputStream;
        }

        public void WriteLine(string line)
        {
            if ((line = transformerChain.Transform(line)) == null)
            {
                return;
            }

            outputStream.WriteLine(line);
        }

        public void Flush()
        {
            outputStream.Flush();
        }
    }
}