using Compiler.Event;
using Compiler.Input.Rule;

namespace Compiler.Input.Event
{
    public class FilesetEmptyEvent: ICompilerEvent
    {
        private readonly IRuleDescriptor ruleDescriptor;

        public FilesetEmptyEvent(IRuleDescriptor ruleDescriptor)
        {
            this.ruleDescriptor = ruleDescriptor;
        }

        public string GetMessage()
        {
            return $"Fileset is empty, details: {ruleDescriptor}";
        }

        public bool IsFatal()
        {
            return false;
        }
    }
}
