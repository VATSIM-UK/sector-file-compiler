using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.Input
{
    public interface IFileInterface
    {
        bool Exists();

        string GetPath();

        bool IsWritable();
    }
}
