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

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public bool IsWritable()
        {
            if (this.Exists())
            {
                FileInfo info = new FileInfo(this.path);
                return FileAttributes.ReadOnly != info.Attributes;
            } else
            {
                try
                {
                    File.Create(this.path).Dispose();
                    File.Delete(this.path);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        public string Contents()
        {
            return File.ReadAllText(this.path);
        }

        public string DirectoryLocation()
        {
            return new FileInfo(this.path).Directory.FullName;
        }

        public List<string> GetAllLines()
        {
            return new List<string>(File.ReadAllLines(this.path));
        }

        public string ParentFolder()
        {
            return Directory.GetParent(this.GetPath()).Name;
        }

        public string GetNameWithoutExtension()
        {
            return Path.GetFileNameWithoutExtension(this.GetPath());
        }
    }
}
