using Compiler.Event;

namespace Compiler.Error
{
    public class UnconfiguredConfigSectionWarning: ICompilerEvent
    {
        private readonly string section;

        public UnconfiguredConfigSectionWarning(string section)
        {
            this.section = section;
        }

        public string GetMessage()
        {
            return $"Unconfigured configuration section {section}";
        }

        public bool IsFatal()
        {
            return false;
        }
    }
}
