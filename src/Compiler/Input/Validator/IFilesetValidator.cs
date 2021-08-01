using System.Collections.Generic;
using Compiler.Event;
using Compiler.Input.Rule;

namespace Compiler.Input.Validator
{
    public interface IFileSetValidator
    {
        /**
         * Filter the files bas
         */
        public void Validate(IEnumerable<string> fileset, IRuleDescriptor ruleDescriptor, IEventLogger eventLogger);
    }
}
