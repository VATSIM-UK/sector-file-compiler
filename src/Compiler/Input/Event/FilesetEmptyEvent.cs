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

        public virtual string GetMessage()
        {
            return $"Fileset is empty for include rule: {ruleDescriptor.GetDescriptor()}";
        }

        public virtual bool IsFatal()
        {
            return false;
        }
    }
}
