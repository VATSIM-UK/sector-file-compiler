using Bogus;
using Compiler.Model;

namespace CompilerTest.Bogus.Factory
{
    static class CommentFactory
    {
        public static Comment Make()
        {
            return new Faker<Comment>()
                .CustomInstantiator(f => new Comment(f.Random.String2(15))).Generate();
        }
    }
}
