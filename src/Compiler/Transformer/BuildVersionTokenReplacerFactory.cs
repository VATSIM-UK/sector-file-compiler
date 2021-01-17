using Compiler.Argument;

namespace Compiler.Transformer
{
    public static class BuildVersionTokenReplacerFactory
    {
        public static BuildVersionTokenReplacer Make(CompilerArguments arguments)
        {
            return new BuildVersionTokenReplacer(arguments);
        }
    }
}