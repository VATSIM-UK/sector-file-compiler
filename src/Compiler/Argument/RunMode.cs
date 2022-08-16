using System;

namespace Compiler.Argument
{
    [Flags]
    public enum RunMode : ushort
    {
        CHECK_CONFIG = 1,
        LINT = 2,
        VALIDATE = 4,
        COMPILE = 8
    }
}
