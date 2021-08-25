﻿using System.Collections.Generic;
using System.IO;

namespace Compiler.Input.Generator
{
    public class RecursiveFolderFileListGenerator: IFileListGenerator
    {
        private readonly string directory;

        public RecursiveFolderFileListGenerator(string directory)
        {
            this.directory = directory;
        }
        
        public IEnumerable<string> GetPaths()
        {
            if (directory.Contains("Agreements\\External"))
            {
                bool test = true;
            }
            
            return Directory.GetFiles(
                directory,
                "*.*",
                SearchOption.AllDirectories
            );
        }
    }
}
