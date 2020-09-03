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
            this.CurrentLine = 0;
        }

        public override IEnumerator<string> GetEnumerator()
        {
            string line;
            using (StreamReader file = new StreamReader(this.FullPath))
            {
                while ((line = file.ReadLine()) != null)
                {
                    this.CurrentLine++;
                    yield return line;
                }
            }
        }
    }
}
