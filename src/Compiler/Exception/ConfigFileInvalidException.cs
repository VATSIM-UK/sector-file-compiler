using System;
using System.Diagnostics.CodeAnalysis;

namespace Compiler.Exception
{
    [ExcludeFromCodeCoverage]
    public class ConfigFileInvalidException: ArgumentException
    {
        public ConfigFileInvalidException(string message)
            : base(message)
        {

        }
    }
}
