using Compiler.Event;
using Compiler.Input.Rule;

namespace Compiler.Input.Validator
{
    public interface IFileValidator
    {
        /**
         * Filter the files bas
         */
        public bool Validate(string path, IRuleDescriptor ruleDescriptor, IEventLogger eventLogger);
    }
}
