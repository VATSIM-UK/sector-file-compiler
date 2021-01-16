using Xunit;
using Compiler.Error;
using Compiler.Input;
using CompilerTest.Bogus.Factory;

namespace CompilerTest.Error
{
    public class SyntaxErrorTest
    {
        private readonly SectorData data;
        private readonly SyntaxError error;

        public SyntaxErrorTest()
        {
            this.data = SectorDataFactory.Make();
            this.error = new SyntaxError("Fooproblem", this.data);
        }

        [Fact]
        public void TestItIsFatal()
        {
            Assert.True(error.IsFatal());
        }

        [Fact]
        public void TestItHasAMessage()
        {
            Assert.Equal(
                $"Syntax Error: Fooproblem in {this.data.definition.Filename} at line {this.data.definition.LineNumber}",
                this.error.GetMessage()
            );
        }
    }
}
