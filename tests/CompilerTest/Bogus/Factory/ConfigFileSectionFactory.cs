using Bogus;
using Compiler.Config;
using Compiler.Input;

namespace CompilerTest.Bogus.Factory
{
    class ConfigFileSectionFactory
    {
        public static ConfigFileSection Make(string descriptor = "test", string jsonPath = null, InputDataType? dataType = null)
        {
            Randomizer randomizer = new();
            return new ConfigFileSection(
                jsonPath ?? "foo/bar/baz.txt",
                dataType ?? randomizer.Enum<InputDataType>(),
                descriptor
            );
        }
    }
}
