using System.Collections.Generic;
using Compiler.Input;
using System.IO;

namespace Compiler.Output
{
    public class Section
    {
        private readonly List<IFileInterface> inputFiles;

        private readonly string header;

        public Section(List<IFileInterface> inputFiles, string header = null)
        {
            this.inputFiles = inputFiles;
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
                output.WriteLine(file.Contents());
            }

            output.Write("\r\n");
        }
    }
}
