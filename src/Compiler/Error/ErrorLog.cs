using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler.Error
{
    public class ErrorLog
    {
        private readonly List<CompilerError> errors = new List<CompilerError>();

        public void AddError(CompilerError error)
        {
            this.errors.Add(error);
        }

        public CompilerError GetLastError()
        {
            return this.errors[errors.Count - 1];
        }
        public int CountErrors()
        {
            return this.errors.Count;
        }
    }
}
