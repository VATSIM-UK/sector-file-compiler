using System.Collections.Generic;
using Compiler.Input;
using System.IO;
using Compiler.Transformer;

namespace Compiler.Output
{
    public class Section
    {
        private readonly List<IFileInterface> inputFiles;
        private readonly TransformerChain transformers;
        private readonly string header;

        public Section(List<IFileInterface> inputFiles, TransformerChain transformers, string header = null)
        {
            this.inputFiles = inputFiles;
            this.transformers = transformers;
            this.header = header;
        }

        public void WriteToFile(TextWriter output)
        {
            if (this.header != null)
            {
                output.Write(this.header + "\r\n");
            }

            foreach (IFileInterface file in this.inputFiles)
            {
                output.Write(
                    OutputStringGenerator.GenerateOutput(this.transformers.Transform(file.GetAllLines()))
                );
            }

            output.Write("\r\n");
        }
    }
}
