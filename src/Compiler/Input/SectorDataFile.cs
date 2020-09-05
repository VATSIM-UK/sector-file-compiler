using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Compiler.Input
{
    public class SectorDataFile: AbstractSectorDataFile
    {
        public SectorDataFile(string fullPath)
        {
            this.FullPath = fullPath;
        }

        public override IEnumerator<string> GetEnumerator()
        {
            string line;
            using (StreamReader file = new StreamReader(this.FullPath))
            {
                while ((line = file.ReadLine()) != null)
                {
                    this.CurrentLine = line;
                    this.CurrentLineNumber++;
                    yield return this.CurrentLine;
                }
            }
        }
    }
}
