using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace Compiler.Input
{
    public class InputFile : IFileInterface
    {
        private readonly string path;

        public InputFile(string path)
        {
            this.path = path;
        }

        public bool Exists()
        {
            return File.Exists(this.path);
        }

        public string GetPath()
        {
            return this.path;
        }

        public override bool Equals(object obj)
        {
            //Check for null and compare run-time types.
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }

            return this.path == ((InputFile)obj).GetPath();
        }
    }
}
