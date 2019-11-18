using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.Input
{
    public interface IFileInterface
    {
        bool Exists();

        string GetPath();

        string GetNameWithoutExtension();

        bool IsWritable();

        string Contents();

        string DirectoryLocation();
        List<string> GetAllLines();

        string ParentFolder();
    }
}
