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
            return $"Validation rule not met: {error}, defined in {definable.GetDefinition()}";
        }

        public bool IsFatal()
        {
            return true;
        }
    }
}
