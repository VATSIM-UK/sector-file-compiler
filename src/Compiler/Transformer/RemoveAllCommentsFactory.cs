using Compiler.Argument;

namespace Compiler.Transformer
{
    public static class RemoveAllCommentsFactory
    {
        public static RemoveAllComments Make(CompilerArguments arguments)
        {
            return new RemoveAllComments(arguments.StripComments);
        }
    }
}