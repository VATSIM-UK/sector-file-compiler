using Bogus;
using Compiler.Model;

namespace CompilerTest.Bogus.Factory
{
    static class DefinitionFactory
    {
        public static Definition Make()
        {
            return new Faker<Definition>()
                .CustomInstantiator(f => new Definition(f.Random.String(), f.Random.Number(1, 100))).Generate();
        }
    }
}
