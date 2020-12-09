using System;
using Compiler.Event;

namespace Compiler.Error
{
    public class ValidationRuleFailure : ICompilerEvent
    {
        private readonly string error;

        public ValidationRuleFailure(string error)
        {
            this.error = error;
        }

        public string GetMessage()
        {
            return String.Format(
                "Validation rule not met: {0}",
                this.error
            );
        }

        public bool IsFatal()
        {
            return true;
        }
    }
}
