using Bogus;
using Compiler.Model;

namespace CompilerTest.Bogus.Factory
{
    static class DefinitionFactory
    {
        public static Definition Make(string filename = null)
        {
            return new Faker<Definition>()
                .CustomInstantiator(f => new Definition(filename ?? f.Random.String2(5) + ".txt", f.Random.Number(1, 100))).Generate();
        }
    }
}
