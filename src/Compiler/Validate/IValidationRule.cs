using Compiler.Event;
using Compiler.Model;
using Compiler.Argument;

namespace Compiler.Validate
{
    public interface IValidationRule
    {
        public void Validate(SectorElementCollection sectorElements, CompilerArguments args, IEventLogger events);
    }
}
