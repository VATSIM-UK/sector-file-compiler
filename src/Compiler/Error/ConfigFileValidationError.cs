using Compiler.Event;

namespace Compiler.Error
{
    public class ConfigFileValidationError : ICompilerEvent
    {
        private readonly string exceptionMessage;

        public ConfigFileValidationError(string exceptionMessage)
        {
            this.exceptionMessage = exceptionMessage;
        }

        public string GetMessage()
        {
            return $"Invalid compiler configuration: {exceptionMessage}";
        }

        public bool IsFatal()
        {
            return true;
        }
    }
}
