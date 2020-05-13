using Compiler.Event;
using Xunit;

namespace CompilerTest.Event
{
    public class ParserSuggestionTest
    {
        private readonly ParserSuggestion message;

        public ParserSuggestionTest()
        {
            this.message = new ParserSuggestion("This is a test suggestion");
        }

        [Fact]
        public void TestItIsNonFatal()
        {
            Assert.False(this.message.IsFatal());
        }

        [Fact]
        public void TestOutput()
        {
            Assert.Equal("SUGGESTION: This is a test suggestion", this.message.GetMessage());
        }
    }
}
