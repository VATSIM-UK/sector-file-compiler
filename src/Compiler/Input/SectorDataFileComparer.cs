using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.Input
{
    public class SectorDataFileComparer : IComparer<AbstractSectorDataFile>
    {
        public int Compare(AbstractSectorDataFile x, AbstractSectorDataFile y)
        {
            return x.CompareTo(y);
        }
    }
}
