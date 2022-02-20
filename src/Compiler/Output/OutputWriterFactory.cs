using System.IO;
using Compiler.Argument;
using Compiler.Transformer;

namespace Compiler.Output
{
    public static class OutputWriterFactory
    {
        public static OutputWriter Make(CompilerArguments arguments, string file, IOutputStreamFactory streamFactory)
        {
            return new(
                TransformerChainFactory.Create(arguments),
                streamFactory.Make(file)
            );
        }
    }
}
