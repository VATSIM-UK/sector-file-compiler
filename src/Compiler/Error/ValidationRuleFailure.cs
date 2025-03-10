using Compiler.Event;
using Compiler.Model;

namespace Compiler.Error
{
    public class ValidationRuleFailure : ICompilerEvent
    {
        private readonly string error;
        private readonly IDefinable definable;

        public ValidationRuleFailure(string error, IDefinable definable)
        {
            this.error = error;
            this.definable = definable;
        }

        public string GetMessage()
        {
            Definition definition = definable.GetDefinition();
            return $"Validation rule not met: {error}, defined at {definition.Filename}:{definition.LineNumber}";
        }

        public bool IsFatal()
        {
            return true;
        }
    }
}
