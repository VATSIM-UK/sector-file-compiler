using System.Collections.Generic;
using System.Linq;
using Compiler.Event;
using Compiler.Input.Event;
using Compiler.Input.Rule;

namespace Compiler.Input.Validator
{
    public class FilelistNotEmpty: IFileSetValidator
    {
        private readonly bool shouldError;

        public FilelistNotEmpty(bool shouldError)
        {
            this.shouldError = shouldError;
        }
        
        public void Validate(IEnumerable<string> fileset, IRuleDescriptor ruleDescriptor, IEventLogger eventLogger)
        {
            if (!fileset.Any())
            {
                eventLogger.AddEvent(
                    shouldError 
                        ? new FilesetEmptyError(ruleDescriptor) 
                        : new FilesetEmptyWarning(ruleDescriptor)
                );
            }
        }
    }
}
