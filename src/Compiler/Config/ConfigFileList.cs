using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Compiler.Input;

namespace Compiler.Config
{
    public class ConfigFileList: IEnumerable<AbstractSectorDataFile>
    {
        private List<AbstractSectorDataFile> files = new List<AbstractSectorDataFile>();

        /*
         * Merge two config files together
         */
        public void Merge(ConfigFileList fileToMerge)
        {
            foreach (AbstractSectorDataFile file in fileToMerge.files)
            {
                this.AddFile(file);
            }
        }

        /*
         * Add a file to the input if it's not already been added.
         */
        public bool AddFile(AbstractSectorDataFile file)
        {
            if (this.files.Contains(file))
            {
                return false;
            }

            this.files.Add(file);
            return true;
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
