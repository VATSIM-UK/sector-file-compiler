using System;

namespace Compiler.Exception
{
    public class ConfigFileInvalidException: ArgumentException
    {
        public ConfigFileInvalidException(string message)
            : base(message)
        {

        }
    }
}
