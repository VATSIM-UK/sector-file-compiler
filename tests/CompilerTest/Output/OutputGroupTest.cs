using Compiler.Output;
using Xunit;

namespace CompilerTest.Output
{
    public class OutputGroupTest
    {
        private readonly OutputGroup groupWithoutDescriptor;
        private readonly OutputGroup groupWithDescriptor;

        public OutputGroupTest()
        {
            this.groupWithoutDescriptor = new OutputGroup("test1");
            this.groupWithDescriptor = new OutputGroup("test1", "descriptor");
        }

        [Fact]
        public void TestItSetsKeyWithoutDescriptor()
        {
            Assert.Equal("test1", this.groupWithoutDescriptor.Key);
        }

        [Fact]
        public void TestItSetDescriptorToNullIfNotProvided()
        {
            Assert.Null(this.groupWithoutDescriptor.HeaderDescription);
        }

        [Fact]
        public void TestItSetsKeyWithDescriptor()
        {
            Assert.Equal("test1", this.groupWithDescriptor.Key);
        }

        [Fact]
        public void TestItSetDescriptorIfProvided()
        {
            Assert.Equal("descriptor", this.groupWithDescriptor.HeaderDescription);
        }

        [Theory]
        [InlineData("test1", "test2", "test1", "test2", true)] // All the same
        [InlineData("test1", "test1", "test1", "test3", true)] // Descriptors different
        [InlineData("test1", "test3", "test1", "test1", true)] // Descriptors different
        [InlineData("test1", "test3", "test2", "test3", false)] // Keys different, descriptors same
        [InlineData("test4", "test1", "test2", "test3", false)] // All different
        public void TestEquality(
            string firstKey,
            string firstDescriptor,
            string secondKey,
            string secondDescriptor,
            bool expectedEqual
        ) {
            Assert.Equal(
                expectedEqual,
                new OutputGroup(firstKey, firstDescriptor).Equals(new OutputGroup(secondKey, secondDescriptor))
            );
        }
    }
}
