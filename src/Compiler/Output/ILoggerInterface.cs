using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.Output
{
    public interface ILoggerInterface
    {
        public void Debug(string message);

        public void Info(string message);

        public void Warning(string message);

        public void Error(string message);
    }
}
