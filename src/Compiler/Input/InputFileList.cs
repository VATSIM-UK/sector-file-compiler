using System.Collections;
using System.Collections.Generic;

namespace Compiler.Input
{
    public class InputFileList: IEnumerable<AbstractSectorDataFile>
    {
        private readonly List<AbstractSectorDataFile> files = new();

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
