using System;

namespace Compiler.Exception
{
    class ConfigFileInvalidException: ArgumentException
    {
        public ConfigFileInvalidException(string message)
            : base(message)
        {

        }
    }
}
