using Bogus;
using Compiler.Model;

namespace CompilerTest.Bogus.Factory
{
    static class DocblockFactory
    {
        public static Docblock Make()
        {
            return new Faker<Docblock>()
                .FinishWith((faker, docblock) =>
                {
                    int numberOfLines = faker.Random.Number(1, 7);
                    for (int i = 0; i < numberOfLines; i++)
                    {
                        docblock.AddLine(CommentFactory.Make());
                    }
                });
        }
    }
}
