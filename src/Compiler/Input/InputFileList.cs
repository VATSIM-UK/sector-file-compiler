using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Compiler.Input
{
    public class InputFileList: IEnumerable<AbstractSectorDataFile>
    {
        private List<AbstractSectorDataFile> files = new List<AbstractSectorDataFile>();

        public void Add(AbstractSectorDataFile file)
        {
            if (this.files.Contains(file))
            {
                return;
            }

            this.files.Add(file);
        }

        public IEnumerator<AbstractSectorDataFile> GetEnumerator()
        {
            return ((IEnumerable<AbstractSectorDataFile>)files).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)files).GetEnumerator();
        }
    }
}
