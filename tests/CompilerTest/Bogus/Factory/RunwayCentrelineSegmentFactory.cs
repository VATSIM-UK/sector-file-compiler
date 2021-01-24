using Bogus;
using Compiler.Model;

namespace CompilerTest.Bogus.Factory
{
    static class RunwayCentrelineSegmentFactory
    {
        public static RunwayCentrelineSegment Make()
        {
            return new Faker<RunwayCentrelineSegment>()
                .CustomInstantiator(
                    _ => new RunwayCentrelineSegment(
                        CoordinateFactory.Make(),
                        CoordinateFactory.Make()
                    )
                );
        }
    }
}
