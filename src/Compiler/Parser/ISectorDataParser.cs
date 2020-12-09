using Compiler.Input;

namespace Compiler.Parser
{
    /*
     * An interface for the classes that parse files
     * to extract data.
     */
    public interface ISectorDataParser
    {
        public void ParseData(AbstractSectorDataFile data);
    }
}
