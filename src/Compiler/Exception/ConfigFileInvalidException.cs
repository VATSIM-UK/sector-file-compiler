using System;
using System.Collections.Generic;
using System.Text;

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
