using Compiler.Argument;
using Compiler.Output;

namespace Compiler
{
    public class SectorFileCompilerFactory
    {
        public static SectorFileCompiler Create(CompilerArguments arguments, IOutputInterface output)
        {
            Logger logger = new Logger(output, arguments);
            return new SectorFileCompiler(
                arguments,
                new CompilerArgumentsValidator(logger),
                logger
            );
        }
    }
}
