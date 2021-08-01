using System.Collections.Generic;
using System.Linq;
using Compiler.Argument;
using Compiler.Event;
using Compiler.Input.Event;
using Compiler.Input.Rule;

namespace Compiler.Input.Validator
{
    public class FilelistNotEmpty: IFileSetValidator
    {
        private readonly int action;

        public FilelistNotEmpty(int action)
        {
            this.action = action;
        }
        
        public void Validate(IEnumerable<string> fileset, IRuleDescriptor ruleDescriptor, IEventLogger eventLogger)
        {
            if (action == CompilerArguments.EmptyFolderIgnore)
            {
                return;
            }
            
            if (!fileset.Any())
            {
                eventLogger.AddEvent(
                    action == CompilerArguments.EmptyFolderError 
                        ? new FilesetEmptyError(ruleDescriptor) 
                        : new FilesetEmptyWarning(ruleDescriptor)
                );
            }
        }
    }
}
