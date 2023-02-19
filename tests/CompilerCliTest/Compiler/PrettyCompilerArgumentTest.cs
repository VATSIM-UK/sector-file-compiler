using System;
using System.Collections.Generic;
using Compiler.Argument;
using CompilerCli.Compiler;
using Xunit;

namespace CompilerCliTest.Compiler;

public class PrettyCompilerArgumentTest
{
    private CompilerArguments arguments;
    private PrettyCompilerArgument prettyCompilerArgument;

    public PrettyCompilerArgumentTest()
    {
        arguments = new CompilerArguments();
        prettyCompilerArgument = new PrettyCompilerArgument();
    }

    [Fact]
    public void TestItSetsPrettyAsMode()
    {
        prettyCompilerArgument.Parse(new List<string>(), arguments);
        Assert.Equal(Pretty.PRETTY, arguments.Pretty);
    }

    [Fact]
    public void TestItThrowsExceptionOnValues()
    {
        Assert.Throws<ArgumentException>(
            () => prettyCompilerArgument.Parse(new List<string>(new[] { "a"}), arguments)
        );
    }
        
    [Fact]
    public void TestItReturnsASpecifier()
    {
        Assert.Equal("--pretty", prettyCompilerArgument.GetSpecifier());
    }
}
