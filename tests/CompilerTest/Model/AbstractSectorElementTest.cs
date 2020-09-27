using System;
using System.Collections.Generic;
using System.Text;
using Compiler.Model;
using Xunit;

namespace CompilerTest.Model
{
    /*
     * Mock class for testing.
     */
    class MockSectorElement : AbstractSectorElement
    {
        public MockSectorElement(Definition definition, string comment) : base(definition, comment)
        {

        }
    }

    public class AbstractSectorElementTest
    {
        private readonly MockSectorElement element;

        public AbstractSectorElementTest()
        {
            this.element = new MockSectorElement(new Definition("foo", 2), "comment");
        }

        [Fact]
        public void TestItReturnsComment()
        {
            Assert.Equal("comment", this.element.Comment);
        }

        [Fact]
        public void TestItReturnsDefinition()
        {
            Assert.Equal(new Definition("foo", 2), this.element.GetDefinition());
        }
    }
}
