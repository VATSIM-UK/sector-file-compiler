using Compiler.Event;
using Compiler.Model;

namespace Compiler.Validate
{
    public interface IValidationRule
    {
        public void Validate(SectorElementCollection sectorElements, IEventLogger events);
    }
}
