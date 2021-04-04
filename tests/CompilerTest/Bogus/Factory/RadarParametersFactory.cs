using Bogus;
using Compiler.Model;

namespace CompilerTest.Bogus.Factory
{
    static class RadarParametersFactory
    {
        public static RadarParameters Make()
        {
            return new Faker<RadarParameters>()
                .CustomInstantiator(
                    f => new RadarParameters(
                        f.Random.Int(0, 3000),
                        f.Random.Int(0, 3000),
                        f.Random.Int(0, 3000)
                    )
                );
        }
    }
}
