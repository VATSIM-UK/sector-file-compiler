using System.Collections.Generic;
using Xunit;
using Compiler.Input;
using Compiler.Setting;

namespace CompilerTest.Setting
{
    public class CompilerSettingsFactoryTest
    {
        [Fact]
        public void ItSetsConfigFilePath()
        {
            Argument argument = new Argument(
                Compiler.Input.ArgumentType.ConfigFile,
                ".\\foo\\bar\\baz.txt"
            );

            CompilerSettings settings = CompilerSettingsFactory.CreateFromArgs(new List<Argument>(new Argument[] { argument }));

            Assert.Equal(".\\foo\\bar\\baz.txt", settings.ConfigFilePath);
        }
    }
}
