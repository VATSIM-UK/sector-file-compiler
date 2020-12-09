using System.Collections.Generic;
using Compiler.Input;

namespace CompilerTest.Mock
{
    class MockSectorDataFile : AbstractSectorDataFile
    {
        private readonly List<string> lines;
        public MockSectorDataFile(string fullPath, List<string> lines)
        {
            this.FullPath = fullPath;
            this.lines = lines;
        }

        public override IEnumerator<string> GetEnumerator()
        {
            foreach (string line in this.lines)
            {
                this.CurrentLine = line;
                this.CurrentLineNumber++;
                yield return this.CurrentLine;
            }
        }
    }
}
