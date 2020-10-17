using System;
using System.Collections.Generic;
using System.Text;
using Compiler.Input;

namespace Compiler.Config
{
    public class ConfigFileList
    {
        public List<AbstractSectorDataFile> Files { get; private set; }

        /*
         * Merge two config files together
         */
        public void Merge(ConfigFileList fileToMerge)
        {
            foreach (AbstractSectorDataFile file in fileToMerge.Files)
            {
                this.AddFile(file);
            }
        }

        /*
         * Add a file to the input if it's not already been added.
         */
        public bool AddFile(AbstractSectorDataFile file)
        {
            if (this.Files.Contains(file))
            {
                return false;
            }

            this.Files.Add(file);
            return true;
        }
    }
}
