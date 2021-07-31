using Compiler.Event;
using Compiler.Input.Rule;

namespace Compiler.Input.Event
{
    public class InputFileDoesNotExist: ICompilerEvent
    {
        private readonly string filename;
        private readonly IRuleDescriptor ruleDescriptor;

        public InputFileDoesNotExist(string filename, IRuleDescriptor ruleDescriptor)
        {
            this.filename = filename;
            this.ruleDescriptor = ruleDescriptor;
        }

        public string GetMessage()
        {
            return $"Input file {filename} does not exist for inclusion rule: {ruleDescriptor.GetDescriptor()}";
        }

        public bool IsFatal()
        {
            return true;
        }
    }
}
